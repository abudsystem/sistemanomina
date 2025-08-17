using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistemanomina.Models;

namespace sistemanomina.Controllers
{
    public class DepartamentoController : Controller
    {
        [HttpGet]
        public ActionResult Departamento()
        {
            return RedirectToAction("Agregar");
        }
        public ActionResult Agregar() => View("Departamento");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Departamento dep)
        {
            if (!ModelState.IsValid) return View("Departamento", dep);
            try
            {
                using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString))
                using (var cmd = new SqlCommand("sp_insertDeparment", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombreDepar", dep.nombreDepar);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
                TempData["ActiveTab"] = "agregar"; // opcional para “pestaña activa”
                return RedirectToAction("Index", "Home");
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                ModelState.AddModelError("", "No se pudo crear el departamento (error de base de datos).");
                return View("Departamento", dep);
            }
        }

        [HttpGet]
        public ActionResult Consultar()
        {
            var dt = new DataTable();

            try
            {
                using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString))
                using (var da = new SqlDataAdapter("sp_getDeparments", cn)) // tu SP
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }

                ViewBag.Departamentos = dt;      // <-- pasamos DataTable
                ViewBag.ActiveTab = "consultar"; // para dejar activa la pestaña
                Session["DeptSection"] = "consultar";
                return View("Departamento");
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                ModelState.AddModelError("", "No se pudo consultar los departamentos.");
                ViewBag.ActiveTab = "consultar";
                return View("Departamento");
            }
        }
        [HttpGet]
        public ActionResult Asignar()
        {
            TempData["ActiveTab"] = "asignar";
            return View("Departamento");
        }

        [HttpGet]
        public ActionResult Historial()
        {
            var dt_historial = new DataTable();

            try
            {
                using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString))
                using (var da = new SqlDataAdapter("sp_getDept_manager", cn)) // tu SP
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt_historial);
                }

                ViewBag.Historial = dt_historial;      // <-- pasamos DataTable historial
                ViewBag.ActiveTab = "historial"; // para dejar activa la pestaña
                Session["DeptSection"] = "historial";
                return View("Departamento");
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                ModelState.AddModelError("", "No se pudo consultar el historial.");
                ViewBag.ActiveTab = "historial";
                return View("Departamento");
            }
        }
    }
}