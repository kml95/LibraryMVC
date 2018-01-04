namespace LibraryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookTag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookTagBooks",
                c => new
                    {
                        BookTag_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookTag_Id, t.Book_Id })
                .ForeignKey("dbo.BookTags", t => t.BookTag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.BookTag_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookTagBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.BookTagBooks", "BookTag_Id", "dbo.BookTags");
            DropIndex("dbo.BookTagBooks", new[] { "Book_Id" });
            DropIndex("dbo.BookTagBooks", new[] { "BookTag_Id" });
            DropTable("dbo.BookTagBooks");
            DropTable("dbo.BookTags");
        }
    }
}
