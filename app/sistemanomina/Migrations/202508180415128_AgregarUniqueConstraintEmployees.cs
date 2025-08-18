namespace sistemanomina.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarUniqueConstraintEmployees : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.employees", "ci", unique: true, name: "ix_employees_ci");
            CreateIndex("dbo.employees", "correo", unique: true, name: "ix_employees_correo");
        }
        
        public override void Down()
        {
            DropIndex("dbo.employees", "ix_employees_correo");
            DropIndex("dbo.employees", "ix_employees_ci");
        }
    }
}
