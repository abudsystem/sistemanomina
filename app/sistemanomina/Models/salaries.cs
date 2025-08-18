namespace sistemanomina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class salaries
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int emp_no { get; set; }

        public long salary { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string from_date { get; set; }

        [StringLength(50)]
        public string to_date { get; set; }

        public virtual employees employees { get; set; }
    }
}
