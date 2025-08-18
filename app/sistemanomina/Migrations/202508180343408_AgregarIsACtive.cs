namespace sistemanomina.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarIsACtive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.departments", "is_active", c => c.Boolean(nullable: false));
            AddColumn("dbo.employees", "is_active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.employees", "is_active");
            DropColumn("dbo.departments", "is_active");
        }
    }
}
