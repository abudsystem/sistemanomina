using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
        public ActionResult Asignar()
        {
            ViewBag.ActiveTab = "asignar";
            return View("Departamento");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Asignar(Departamento dep, string accion, int? emp_no, int? dept_no_actual, int? dept_no_nuevo)
        {
            ViewBag.ActiveTab = "asignar";
            var dt = new DataTable();

            try
            {
                // Consultar 
                if (string.Equals(accion, "consultar", StringComparison.OrdinalIgnoreCase))
                {
                    using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString))
                    using (var cmd = new SqlCommand("sp_getAsigDepartEmpl", cn))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ci", dep.cedulaEmpl ?? (object)DBNull.Value);
                        da.Fill(dt);
                    }

                    ViewBag.Asignar = dt;
                    ViewBag.CedulaEmpl = dep.cedulaEmpl;

                    // Se debe agregar pregunta de fecha null para tomar el departamento actual correcto
                    if (dt.Rows.Count > 0)
                    {
                        var r = dt.Rows[0];

                        // Conecta con SP y utiliza los campos despues nombrados como AS                                         
                        ViewBag.EmpNo = Convert.ToInt32(r["Id"]);
                        ViewBag.DeptNoActual = Convert.ToInt32(r["Id_Departamento"]);

                    }

                    return View("Departamento", dep);
                }

                //  Realiza validaciones y usa los paremetros SP tal cual: @emp_no, @dept_no, @dept_no_nuevo)
                if (string.Equals(accion, "asignar", StringComparison.OrdinalIgnoreCase))
                {
                    if (emp_no == null || dept_no_actual == null || dept_no_nuevo == null)
                    {
                        ModelState.AddModelError("", "Faltan datos. Primero pulsa 'Consultar'.");
                        return View("Departamento", dep);
                    }
                    if (dept_no_actual == dept_no_nuevo)
                    {
                        ModelState.AddModelError("", "El nuevo departamento no puede ser igual al actual.");
                        return View("Departamento", dep);
                    }

                    using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString))
                    using (var cmd = new SqlCommand("sp_setAsigDepartEmpl", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@emp_no", emp_no.Value);
                        cmd.Parameters.AddWithValue("@dept_no", dept_no_actual.Value);
                        cmd.Parameters.AddWithValue("@dept_no_nuevo", dept_no_nuevo.Value);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    TempData["ok"] = "Asignación realizada.";
                    // Se podria reconsultar para actualizar la tabla
                    return RedirectToAction("Asignar");
                }

                // Fallback
                ModelState.AddModelError("", "Acción no reconocida.");
                return View("Departamento", dep);
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                ModelState.AddModelError("", "Error de base de datos.");
                return View("Departamento", dep);
            }
        }
       

        // Historial de cambios de departamentos por empleados

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