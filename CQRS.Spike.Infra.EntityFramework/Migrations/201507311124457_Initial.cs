namespace CQRS.Spike.Infra.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aggregates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Version = c.Int(nullable: false),
                        Data = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.Version })
                .ForeignKey("dbo.Aggregates", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Id", "dbo.Aggregates");
            DropIndex("dbo.Events", new[] { "Id" });
            DropTable("dbo.Events");
            DropTable("dbo.Aggregates");
        }
    }
}
