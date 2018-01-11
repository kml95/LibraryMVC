namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Histories",
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
            DropForeignKey("dbo.Histories", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Histories", "Book_Id", "dbo.Books");
            DropIndex("dbo.Histories", new[] { "User_Id" });
            DropIndex("dbo.Histories", new[] { "Book_Id" });
            DropTable("dbo.Histories");
        }
    }
}
