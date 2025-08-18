namespace sistemanomina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employees()
        {
            dept_emp = new HashSet<dept_emp>();
            dept_manager = new HashSet<dept_manager>();
            Log_AuditoriaSalarios = new HashSet<Log_AuditoriaSalarios>();
            salaries = new HashSet<salaries>();
            titles = new HashSet<titles>();
        }

        [Key]
        public int emp_no { get; set; }

        [Required]
        [StringLength(50)]
        [Index("ix_employees_ci", IsUnique=true)] // restruccion unique
        public string ci { get; set; }

        [Required]
        [StringLength(50)]
        public string birth_date { get; set; }

        [Required]
        [StringLength(50)]
        public string first_name { get; set; }

        [Required]
        [StringLength(50)]
        public string last_name { get; set; }

        [Required]
        [StringLength(1)]
        public string gender { get; set; }

        [Required]
        [StringLength(50)]
        public string hire_date { get; set; }

        [Required]
        [StringLength(120)]
        [Index("ix_employees_correo", IsUnique = true)]
        public string correo { get; set; }

        public bool is_active { get; set; } = true; // por defecto 1


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dept_emp> dept_emp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dept_manager> dept_manager { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log_AuditoriaSalarios> Log_AuditoriaSalarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<salaries> salaries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<titles> titles { get; set; }

        public virtual users users { get; set; }
    }
}
