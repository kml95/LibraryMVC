namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaaa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lends", "DateBorrowed", c => c.DateTime());
            AlterColumn("dbo.Lends", "DateReturn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lends", "DateReturn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Lends", "DateBorrowed", c => c.DateTime(nullable: false));
        }
    }
}
