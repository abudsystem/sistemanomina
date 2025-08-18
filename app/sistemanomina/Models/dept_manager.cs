namespace sistemanomina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class dept_manager
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int emp_no { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int dept_no { get; set; }

        [Required]
        [StringLength(50)]
        public string from_date { get; set; }

        [Required]
        [StringLength(50)]
        public string to_date { get; set; }

        public virtual departments departments { get; set; }

        public virtual employees employees { get; set; }
    }
}
