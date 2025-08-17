/*
	Documento con objetos pertenecientes a la base de datos
	nominaDB
*/

USE nominaDB
GO

-- OBJETOS

/*Generar un informa de todos los empleados de la empresa
incluye su ci, nombre, apellido y fecha de ingreso*/

CREATE OR ALTER VIEW VW_EMPLOYEES_REPRT AS
	SELECT
		ci,
		first_name,
		last_name,
		hire_date
	FROM
		employees
GO

-- Generar un informe de todos los empleados que ganen mas de $2000
CREATE OR ALTER VIEW VW_SALARY_REPORT AS
SELECT
	e.emp_no, 
	e.first_name,
	e.last_name,
	s.from_date,
	s.to_date
FROM
	(
		SELECT *,
			ROW_NUMBER() OVER (PARTITION BY s.emp_no ORDER BY s.from_date DESC) AS rn
		FROM salaries s
		WHERE salary > 2000
	) AS s
JOIN employees e ON e.emp_no = s.emp_no
WHERE s.rn = 1 
GO

/*====================================
�		STORED PROCEDURES
=====================================*/
CREATE OR ALTER PROCEDURE sp_departmentInformation 
AS
	select * from departments
GO

-- Visualizar todos los cargos que ha obtenido un empleado

CREATE OR ALTER PROCEDURE sp_EmployeesTitles @ci varchar(50) AS
SELECT
	e.first_name, e.last_name,
	t.title, t.from_date, t.to_date
FROM employees e
INNER JOIN titles t
ON e.emp_no = t.emp_no
WHERE e.ci = @ci
GO

EXEC sp_EmployeesTitles '1709846732'
GO

-- Simular la actualizacion del salario de un empleado

-- Parametros
--	ci
--	salary
--	message

CREATE OR ALTER PROCEDURE sp_salaryUpdate
	@emp_no INT,
	@salary INT,
	@message varchar(150) output
AS
	BEGIN
		--validacion de xistencia de usuario
		IF NOT EXISTS (SELECT * FROM employees WHERE emp_no = emp_no)
			BEGIN
				SET @message = 'No existe registro del empleado'
			END
		ELSE 
			BEGIN
				IF NOT EXISTS (SELECT * FROM salaries WHERE emp_no=emp_no)
					BEGIN
						INSERT INTO salaries (emp_no, salary, from_date, to_date)
						VALUES
							(@emp_no, 1795, CONVERT(VARCHAR(50), GETDATE(), 103), CONVERT(VARCHAR(50), GETDATE()+1000, 103))
						set @message = 'El salario del empleado ha sido agregado'
					END
				ELSE
					BEGIN
						UPDATE salaries
						SET salary = @salary WHERE emp_no = @emp_no

						set @message = 'El salario del empleado ha sido actualizado'
					END
			END
	END
GO

declare @mensaje varchar(150)
exec sp_SalaryUpdate 2, 4500, @mensaje output
select @mensaje

select * from salaries
GO


CREATE OR ALTER PROCEDURE sp_salaryTitles
@emp_no INT,
@message VARCHAR(250) output
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM employees WHERE emp_no = @emp_no)
		BEGIN
			SET @message = 'Empleado no encontrado'
		END
	ELSE
		BEGIN
			SELECT
				e.first_name,
				e.last_name,
				t.title,
				t.from_date AS title_from,
				t.to_date AS title_to,
				s.salary,
				s.from_date AS salary_from,
			    s.to_date AS salary_to
			FROM employees e
			INNER JOIN titles t
			ON e.emp_no = t.emp_no
			INNER JOIN salaries s
			ON e.emp_no = s.emp_no
				AND s.from_date <= t.to_date
				AND s.to_date >= t.from_date
			WHERE e.emp_no = @emp_no
		END

END
go

declare @message varchar(250)
exec sp_salaryTitles 8, @message output
select @message
GO
-- TRIGGERS
-- Accion sobre una tabla (UPDATE, DELETE, INSERT, SELECT)
-- Antes o despues de realizar la accion sobre la tabla
-- generalmente sirve para insertar informacion en la tabla
-- utiles en controles de auditoria

-- RF02: Cada vez que se cree un registro del salario de un empleado, necesito conocer la auditoria de quien crea el registro

-- Quien hizo el cambio
-- cuando lo hizo
-- cual fue el cambio

	-- usuario
	-- fecha de cambio
	-- detalle del cambio
	-- salario cambiado
	-- codigo del empleado


-- creacion del trigger
CREATE OR ALTER TRIGGER tr_employeeChangeSalary
	on salaries
	after insert
AS
	BEGIN
		DECLARE @salary bigint
		DECLARE @emp_no int

		--tomando los valores durante la accion en la tabla
		set @emp_no = (SELECT emp_no FROM inserted)
		set @salary = (SELECT salary FROM inserted)

		-- insertar datos en la tabla de auditoria de salarios
		INSERT INTO log_auditorySalary (username, update_date, change_detail, salary, emp_no)
		VALUES
			(SUSER_SNAME(), GETDATE(), 'Nuevo empleado', @salary, @emp_no)
	END
GO

INSERT INTO employees (ci, birth_date, first_name, last_name, gender, hire_date)
VALUES
    ('1708913621', '1998-07-12', 'Daniela', 'Albertoni', 'F', Getdate())

INSERT INTO dept_emp (emp_no, dept_no, from_date, to_date)
VALUES
	(31, 5, getdate(), '2050-12-31')

INSERT INTO titles (emp_no, title, from_date, to_date)
VALUES
	(31, 'Analista de TI', getdate(), '2050-12-31')

INSERT INTO salaries (emp_no, salary, from_date, to_date)
VALUES
  (31, 1700, getdate(), '2050-12-31')

select * from salaries where emp_no = 31
select * from log_auditorySalary
Go


-- Procedure para Autenticacion de usuarios
-- Procedure de autenticacion
CREATE OR ALTER PROCEDURE sp_userAuthentication
@usuario varchar(150),
@clave varchar(10),
@message varchar(100) output
AS
Begin
	If Not Exists (Select * from employees e join users u on e.emp_no = u.emp_no Where e.correo = @usuario)
	Begin
		Set @message = 'El usuario ingresado no existe en el sistema'
	End
	Else
	Begin
		If Not Exists (Select * From users Where clave = @clave)
		Begin
			Set @message = 'El password es incorrecto'
		End
		Else
		Begin
			set @message = 'Autenticacion exitosa'
			Select 
				u.usuario, 
				concat(e.first_name, ' ', e.last_name) As nombre,
				e.correo, 
				e.ci as ci
			From users u
			join employees e
			on e.emp_no = u.emp_no
			where e.correo = @usuario
				And clave = @clave
		End
	End
End
Go

-- Usuario existente
declare @message varchar(100)
Exec sp_userAuthentication 'rocio.espinoza@correo.com', 'usu2021', @message output
Select @message
go

select * from employees
go

--==========================================
-- Procedure ingresar empleado y usuarios
--==========================================
CREATE OR ALTER PROCEDURE sp_insertEmployee
@ci varchar(10),
@fechaNacimiento varchar(20),
@nombre varchar(50),
@apellido varchar(50),
@correo varchar(100),
@genero char(1),
@clave varchar(12)
AS
BEGIN
	-- insercion de empelado
	INSERT INTO employees (ci, birth_date, first_name, last_name, gender, hire_date, correo)
	VALUES
		(@ci,
		@fechaNacimiento,
		@nombre,
		@apellido,
		@genero,
		CONVERT(VARCHAR(100), GETDATE(), 103),
		@correo)

	-- OBTENER ID DE EMPLEADO
	DECLARE @emp_no int = SCOPE_IDENTITY();

	-- generacion de usuario automatico
	DECLARE @usr VARCHAR(40);
	SET @usr = LOWER(LEFT(@nombre, 1)+@apellido);

	--insertar usuario
	INSERT INTO users (emp_no, usuario, clave)
	values
		(@emp_no, @usr, @clave)

END
GO

exec sp_insertEmployee '1245763300', '12/09/2001', 'Federico', 'Navas', 
				'federico.n@correo.com', 'M', 'federico19'

go

-- procedure para lista todos los empleados
CREATE OR ALTER PROCEDURE sp_getEmployees
AS
BEGIN
	select emp_no, ci, first_name, last_name, birth_date,
		   hire_date, correo, gender
	from employees
END
go

EXEC sp_getEmployees

select * from employees
select * from users