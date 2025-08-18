using sistemanomina.Models;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace sistemanomina.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
        public ActionResult About() { ViewBag.Message = "Your application description page."; return View(); }
        public ActionResult Contact() { ViewBag.Message = "Your contact page."; return View(); }

        public ActionResult Autenticacion() { ViewBag.Message = "Pagina de Autenticacion"; return View(); }
        public ActionResult Empleados() { ViewBag.Message = "Pagina de Empleados"; return View(); }
        public ActionResult Departamentos() { ViewBag.Message = "Pagina de Departamentos"; return View(); }
        public ActionResult AsignacionADepartamentos() { ViewBag.Message = "Pagina de asignacion a departamentos"; return View(); }
        public ActionResult GerentesDeDepartamento() { ViewBag.Message = "Pagina de los Gerentes de departamento"; return View(); }
        public ActionResult TitulosCargos() { ViewBag.Message = "Pagina de los ttulos / Cargos"; return View(); }
        public ActionResult Salarios() { ViewBag.Message = "Pagina de Salarios"; return View(); }
        public ActionResult Reportes() { ViewBag.Message = "Pagina de los Reportes"; return View(); }
        public ActionResult Administracion() { ViewBag.Message = "Pagina de Administracion"; return View(); }

        public ActionResult ProbarConexion()
        {
            using (var db = new NominaContext())
            {
                try
                {
                    int cantidad = db.Empleados.Count();
                    ViewBag.Message = "Conexión exitosa. Empleados registrados: " + cantidad;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error en la conexión: " + ex.Message;
                }
            }
            return View();
        }

        // ========================================
        // Acción para mostrar detalle del empleado
        // ========================================
        public ActionResult DetalleEmpleado(int id)
        {
            var empleado = ObtenerEmpleadoPorId(id);

            if (empleado == null)
                return HttpNotFound();

            return View(empleado);
        }

        // ========================================
        // Método que llama al SP usando EF6
        // ========================================
        
        /*
         * private Empleado ObtenerEmpleadoPorId(int id)
        {
            using (var context = new NominaContext())
            {
                var empleado = context.Database.SqlQuery<Empleado>(
                    "EXEC sp_ObtenerEmpleadoPorID @Id = {1}", id
                ).FirstOrDefault();

                return empleado;
            }
        }
        */
        private Empleado ObtenerEmpleadoPorId(int id)
        {
            using (var context = new NominaContext())
            {
                var param = new SqlParameter("@Id", id);

                var empleado = context.Database.SqlQuery<Empleado>(
                    "EXEC sp_ObtenerEmpleadoPorID @Id", param
                ).FirstOrDefault();

                return empleado;
            }
        }
    }
}
