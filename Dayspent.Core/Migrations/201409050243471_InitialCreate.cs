namespace Dayspent.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        TimelineId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Description = c.String(nullable: false),
                        TimeSpent = c.String(maxLength: 20),
                        TimeSpentMins = c.Int(),
                        ActivityByUserId = c.String(nullable: false, maxLength: 130),
                        TenantId = c.Int(nullable: false),
                        CreatedByUserId = c.String(nullable: false, maxLength: 130),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.String(nullable: false, maxLength: 130),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityId)
                .ForeignKey("dbo.Timelines", t => t.TimelineId, cascadeDelete: true)
                .Index(t => t.TimelineId);
            
            CreateTable(
                "dbo.ActivityTags",
                c => new
                    {
                        ActivityTagId = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CreatedByUserId = c.String(nullable: false, maxLength: 130),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.String(nullable: false, maxLength: 130),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityTagId)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ActivityId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        IsPrivate = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CreatedByUserId = c.String(nullable: false, maxLength: 130),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.String(nullable: false, maxLength: 130),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.Timelines",
                c => new
                    {
                        TimelineId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        OwnerId = c.String(nullable: false, maxLength: 130),
                        TenantId = c.Int(nullable: false),
                        CreatedByUserId = c.String(nullable: false, maxLength: 130),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.String(nullable: false, maxLength: 130),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TimelineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "TimelineId", "dbo.Timelines");
            DropForeignKey("dbo.ActivityTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.ActivityTags", "ActivityId", "dbo.Activities");
            DropIndex("dbo.ActivityTags", new[] { "TagId" });
            DropIndex("dbo.ActivityTags", new[] { "ActivityId" });
            DropIndex("dbo.Activities", new[] { "TimelineId" });
            DropTable("dbo.Timelines");
            DropTable("dbo.Tags");
            DropTable("dbo.ActivityTags");
            DropTable("dbo.Activities");
        }
    }
}
