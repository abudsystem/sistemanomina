using System.Data.Entity;

namespace sistemanomina.Models
{
    public class NominaContext : DbContext
    {
        // Constructor que llama a la cadena de conexión de Web.config
        public NominaContext() : base("NominaDB")
        {
            // Inicializador que elimina y recrea la base de datos si cambia el modelo
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NominaContext>());
        }

        // Aquí se agregan las tablas (DbSet = tabla en SQL Server)
        public DbSet<Empleado> Empleados { get; set; }
    }
}
/*

 using System.Data.Entity;

namespace sistemanomina.Models
{
    public class NominaContext : DbContext
    {
        // Constructor que llama a la cadena de conexión de Web.config
        public NominaContext() : base("AppDbContext")
        {
            // Nota: NO necesitamos el DropCreateDatabaseIfModelChanges si usamos migraciones
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NominaContext>());
        }

        // Tablas del modelo
        public DbSet<Empleado> Empleados { get; set; }
        // Aquí puedes agregar otros DbSet, por ejemplo:
        // public DbSet<Departamento> Departamentos { get; set; }
        // public DbSet<Salario> Salarios { get; set; }
    }
}
*/

