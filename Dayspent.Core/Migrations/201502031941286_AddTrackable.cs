namespace Dayspent.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrackable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trackables",
                c => new
                    {
                        TrackableId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 120),
                        TrackableType = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CreatedByUserId = c.String(nullable: false, maxLength: 130),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.String(nullable: false, maxLength: 130),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TrackableId);
            
            AddColumn("dbo.StatusReportItems", "TrackableId", c => c.Int());
            CreateIndex("dbo.StatusReportItems", "TrackableId");
            AddForeignKey("dbo.StatusReportItems", "TrackableId", "dbo.Trackables", "TrackableId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusReportItems", "TrackableId", "dbo.Trackables");
            DropIndex("dbo.StatusReportItems", new[] { "TrackableId" });
            DropColumn("dbo.StatusReportItems", "TrackableId");
            DropTable("dbo.Trackables");
        }
    }
}
