namespace Person_Information_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPersonAddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressType = c.String(nullable: false),
                        Province = c.String(nullable: false),
                        Zone = c.String(nullable: false),
                        District = c.String(nullable: false),
                        Ward = c.String(nullable: false),
                        Tole = c.String(nullable: false),
                        PersonInfo_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.PersonInfoes", t => t.PersonInfo_PersonId)
                .Index(t => t.PersonInfo_PersonId);
            
            CreateTable(
                "dbo.PersonInfoes",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Salutation = c.String(nullable: false),
                        FirstName = c.String(),
                        Email = c.String(nullable: false),
                        Age = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Education = c.String(nullable: false),
                        AddressId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "PersonInfo_PersonId", "dbo.PersonInfoes");
            DropIndex("dbo.Addresses", new[] { "PersonInfo_PersonId" });
            DropTable("dbo.PersonInfoes");
            DropTable("dbo.Addresses");
        }
    }
}
