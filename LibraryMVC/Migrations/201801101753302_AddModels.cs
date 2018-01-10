namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateBorrowed = c.DateTime(nullable: false),
                        DateReturn = c.DateTime(nullable: false),
                        State = c.String(),
                        Book_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Queues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Book_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Queues", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Queues", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.Lends", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lends", "Book_Id", "dbo.Books");
            DropIndex("dbo.Queues", new[] { "User_Id" });
            DropIndex("dbo.Queues", new[] { "Book_Id" });
            DropIndex("dbo.Lends", new[] { "User_Id" });
            DropIndex("dbo.Lends", new[] { "Book_Id" });
            DropTable("dbo.Queues");
            DropTable("dbo.Lends");
        }
    }
}
