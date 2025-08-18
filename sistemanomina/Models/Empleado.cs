using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sistemanomina.Models
{
    [Table("Empleado")]
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal Salario { get; set; }
        public string sexo { get; set; }
    }
}