using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistemanomina.Models; // Asegúrate de que este espacio de nombres sea correcto
using System.Data.Entity;   // para incluir EF

namespace sistemanomina.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // Aqui es todo lo creado
        public ActionResult Autenticacion()
        {
            ViewBag.Message = "Pagina de Autenticacion";
            return View();
        }
        public ActionResult Empleados()
        {
            ViewBag.Message = "Pagina de Empleados";
            return View();
        }
        public ActionResult Departamentos()
        {
            ViewBag.Message = "Pagina de Departamentos";
            return View();
        }
        public ActionResult AsignacionADepartamentos()
        {
            ViewBag.Message = "Pagina de asignacion a departamentos";
            return View();
        }
        public ActionResult GerentesDeDepartamento()
        {
            ViewBag.Message = "Pagina de los Gerentes de departamento";
            return View();

        
        }
        public ActionResult TitulosCargos()
        {
            ViewBag.Message = "Pagina de los ttulos / Cargos";
            return View();


        }
        public ActionResult Salarios()
        {
            ViewBag.Message = "Pagina de Salarios";
            return View();


        }
        public ActionResult Reportes()
        {
            ViewBag.Message = "Pagina de los Reportes";
            return View();


        }
        public ActionResult Administracion()
        {
            ViewBag.Message = "Pagina de Administracion";
            return View();


        }
        public ActionResult ProbarConexion()
        {
            using (var db = new NominaContext())
            {
                var count = db.Empleados.Count(); // Esto fuerza la creación de la DB
                try
                {
                    
                    // Cuenta los empleados de la tabla

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
    }
}