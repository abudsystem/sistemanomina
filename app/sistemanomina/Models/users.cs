namespace sistemanomina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int emp_no { get; set; }

        [Required]
        [StringLength(150)]
        public string usuario { get; set; }

        [Required]
        [StringLength(20)]
        public string clave { get; set; }

        [Required]
        [StringLength (20)]
        public string rol {  get; set; }

        public virtual employees employees { get; set; }
    }
}
