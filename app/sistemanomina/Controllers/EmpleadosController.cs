using sistemanomina.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistemanomina.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly NominaDBEntities nominaDB = new NominaDBEntities();
        // GET: Empleados
        public ActionResult Index(string query, string estado = "Todos", int pagina = 1, int elementosPagina = 15)
        {
            // parámetros para el SP
            var queryParam = new SqlParameter("@query", string.IsNullOrEmpty(query) ? DBNull.Value : (object)query);
            var estadoParam = new SqlParameter("@estado", string.IsNullOrEmpty(estado) ? "Todos" : estado);

            // ejecutamos el SP
            var empleados = nominaDB.Database.SqlQuery<employees>(
                "EXEC sp_getEmployees @query, @estado",
                queryParam,
                estadoParam
            ).ToList();

            // paginación manual
            int totalEmpleados = empleados.Count;
            var empleadosPagina = empleados
                .OrderBy(e => e.emp_no)
                .Skip((pagina - 1) * elementosPagina)
                .Take(elementosPagina)
                .ToList();

            // datos para la vista
            ViewBag.CurrentPage = pagina;
            ViewBag.PageSize = elementosPagina;
            ViewBag.TotalItems = totalEmpleados;
            ViewBag.Search = query;
            ViewBag.Estado = estado;

            return View(empleadosPagina);
        }

        [HttpPost]
        public ActionResult CambiarEstado(int empNo)
        {
            var emp = nominaDB.employees.Find(empNo);
            if (emp != null)
            {
                emp.is_active = !emp.is_active;
                nominaDB.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
