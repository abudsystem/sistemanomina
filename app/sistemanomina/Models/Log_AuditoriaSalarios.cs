namespace sistemanomina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log_AuditoriaSalarios
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_actualizacion { get; set; }

        [Required]
        [StringLength(250)]
        public string detalle_cambio { get; set; }

        public long salario { get; set; }

        public int emp_no { get; set; }

        public virtual employees employees { get; set; }
    }
}
