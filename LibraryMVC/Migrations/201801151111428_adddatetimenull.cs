namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddatetimenull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lends", "DateReturn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lends", "DateReturn", c => c.DateTime(nullable: false));
        }
    }
}
