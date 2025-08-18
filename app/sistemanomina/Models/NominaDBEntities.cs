using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace sistemanomina.Models
{
    public partial class NominaDBEntities : DbContext
    {
        public NominaDBEntities()
            : base("name=NominaDBEntities")
        {
        }

        public virtual DbSet<departments> departments { get; set; }
        public virtual DbSet<dept_emp> dept_emp { get; set; }
        public virtual DbSet<dept_manager> dept_manager { get; set; }
        public virtual DbSet<employees> employees { get; set; }
        public virtual DbSet<Log_AuditoriaSalarios> Log_AuditoriaSalarios { get; set; }
        public virtual DbSet<salaries> salaries { get; set; }
        public virtual DbSet<titles> titles { get; set; }
        public virtual DbSet<users> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<departments>()
                .Property(e => e.dept_name)
                .IsUnicode(false);

            modelBuilder.Entity<departments>()
                .HasMany(e => e.dept_emp)
                .WithRequired(e => e.departments)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<departments>()
                .HasMany(e => e.dept_manager)
                .WithRequired(e => e.departments)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<dept_emp>()
                .Property(e => e.from_date)
                .IsUnicode(false);

            modelBuilder.Entity<dept_emp>()
                .Property(e => e.to_date)
                .IsUnicode(false);

            modelBuilder.Entity<dept_manager>()
                .Property(e => e.from_date)
                .IsUnicode(false);

            modelBuilder.Entity<dept_manager>()
                .Property(e => e.to_date)
                .IsUnicode(false);

            modelBuilder.Entity<employees>()
                .Property(e => e.ci)
                .IsUnicode(false);

            modelBuilder.Entity<employees>()
                .Property(e => e.birth_date)
                .IsUnicode(false);

            modelBuilder.Entity<employees>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<employees>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<employees>()
                .Property(e => e.gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<employees>()
                .Property(e => e.hire_date)
                .IsUnicode(false);

            modelBuilder.Entity<employees>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<employees>()
                .HasMany(e => e.dept_emp)
                .WithRequired(e => e.employees)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employees>()
                .HasMany(e => e.dept_manager)
                .WithRequired(e => e.employees)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employees>()
                .HasMany(e => e.Log_AuditoriaSalarios)
                .WithRequired(e => e.employees)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employees>()
                .HasMany(e => e.salaries)
                .WithRequired(e => e.employees)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employees>()
                .HasMany(e => e.titles)
                .WithRequired(e => e.employees)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employees>()
                .HasOptional(e => e.users)
                .WithRequired(e => e.employees);

            modelBuilder.Entity<Log_AuditoriaSalarios>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Log_AuditoriaSalarios>()
                .Property(e => e.detalle_cambio)
                .IsUnicode(false);

            modelBuilder.Entity<salaries>()
                .Property(e => e.from_date)
                .IsUnicode(false);

            modelBuilder.Entity<salaries>()
                .Property(e => e.to_date)
                .IsUnicode(false);

            modelBuilder.Entity<titles>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<titles>()
                .Property(e => e.from_date)
                .IsUnicode(false);

            modelBuilder.Entity<titles>()
                .Property(e => e.to_date)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.clave)
                .IsUnicode(false);
        }
    }
}
