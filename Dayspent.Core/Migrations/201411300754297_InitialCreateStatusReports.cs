namespace Dayspent.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateStatusReports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportingGroups",
                c => new
                    {
                        ReportingGroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        TenantId = c.Int(nullable: false),
                        CreatedByUserId = c.String(nullable: false, maxLength: 130),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.String(nullable: false, maxLength: 130),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReportingGroupId);
            
            CreateTable(
                "dbo.ReportingGroupMembers",
                c => new
                    {
                        ReportingGroupMemberId = c.Int(nullable: false, identity: true),
                        ReportingGroupId = c.Int(nullable: false),
                        MemberUserId = c.String(nullable: false, maxLength: 130),
                        TenantId = c.Int(nullable: false),
                        CreatedByUserId = c.String(nullable: false, maxLength: 130),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.String(nullable: false, maxLength: 130),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReportingGroupMemberId)
                .ForeignKey("dbo.ReportingGroups", t => t.ReportingGroupId, cascadeDelete: true)
                .Index(t => t.ReportingGroupId);
            
            CreateTable(
                "dbo.StatusReportCategories",
                c => new
                    {
                        StatusReportCategoryId = c.Int(nullable: false, identity: true),
                        Sequence = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 200),
                        Remarks = c.String(),
                        TenantId = c.Int(nullable: false),
                        CreatedByUserId = c.String(nullable: false, maxLength: 130),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.String(nullable: false, maxLength: 130),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StatusReportCategoryId);
            
            CreateTable(
                "dbo.StatusReportItems",
                c => new
                    {
                        StatusReportItemId = c.Int(nullable: false, identity: true),
                        StatusReportId = c.Int(nullable: false),
                        StatusReportCategoryId = c.Int(nullable: false),
                        Sequence = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        TimeSpentInSecs = c.Int(),
                        HasRedFlag = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CreatedByUserId = c.String(nullable: false, maxLength: 130),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.String(nullable: false, maxLength: 130),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StatusReportItemId)
                .ForeignKey("dbo.StatusReports", t => t.StatusReportId, cascadeDelete: true)
                .ForeignKey("dbo.StatusReportCategories", t => t.StatusReportCategoryId, cascadeDelete: true)
                .Index(t => t.StatusReportId)
                .Index(t => t.StatusReportCategoryId);
            
            CreateTable(
                "dbo.StatusReports",
                c => new
                    {
                        StatusReportId = c.Int(nullable: false, identity: true),
                        ReportDate = c.DateTime(nullable: false),
                        ReportingUserId = c.String(nullable: false, maxLength: 130),
                        SubmittedDate = c.DateTime(),
                        Suggestions = c.String(),
                        TenantId = c.Int(nullable: false),
                        CreatedByUserId = c.String(nullable: false, maxLength: 130),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.String(nullable: false, maxLength: 130),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StatusReportId);
            
            CreateTable(
                "dbo.StatusReportItemTags",
                c => new
                    {
                        StatusReportItemTagId = c.Int(nullable: false, identity: true),
                        StatusReportItemId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CreatedByUserId = c.String(nullable: false, maxLength: 130),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.String(nullable: false, maxLength: 130),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StatusReportItemTagId)
                .ForeignKey("dbo.StatusReportItems", t => t.StatusReportItemId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.StatusReportItemId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusReportItemTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.StatusReportItemTags", "StatusReportItemId", "dbo.StatusReportItems");
            DropForeignKey("dbo.StatusReportItems", "StatusReportCategoryId", "dbo.StatusReportCategories");
            DropForeignKey("dbo.StatusReportItems", "StatusReportId", "dbo.StatusReports");
            DropForeignKey("dbo.ReportingGroupMembers", "ReportingGroupId", "dbo.ReportingGroups");
            DropIndex("dbo.StatusReportItemTags", new[] { "TagId" });
            DropIndex("dbo.StatusReportItemTags", new[] { "StatusReportItemId" });
            DropIndex("dbo.StatusReportItems", new[] { "StatusReportCategoryId" });
            DropIndex("dbo.StatusReportItems", new[] { "StatusReportId" });
            DropIndex("dbo.ReportingGroupMembers", new[] { "ReportingGroupId" });
            DropTable("dbo.StatusReportItemTags");
            DropTable("dbo.StatusReports");
            DropTable("dbo.StatusReportItems");
            DropTable("dbo.StatusReportCategories");
            DropTable("dbo.ReportingGroupMembers");
            DropTable("dbo.ReportingGroups");
        }
    }
}
