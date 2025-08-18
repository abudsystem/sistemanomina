namespace sistemanomina.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarRolUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.users", "rol", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.users", "rol");
        }
    }
}
