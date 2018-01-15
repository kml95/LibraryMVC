namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlend : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lends", "DateBorrowed", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lends", "DateBorrowed", c => c.DateTime(nullable: false));
        }
    }
}
