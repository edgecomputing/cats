using FluentMigrator;

namespace Cats.DatabaseMigrations
{
    [Migration(201306211026)]
    public class ReliefRequistionMigration : Migration
    {
        public override void Up()
        {
            Create.Table("ReliefRequisition").InSchema("EarlyWarning")
                  .WithColumn("RequisitionID").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("CommodityID").AsInt32().Nullable()
                  .WithColumn("RegionID").AsInt32().Nullable()
                  .WithColumn("ZoneID").AsInt32().Nullable()
                  .WithColumn("Round").AsInt32().Nullable()
                  .WithColumn("RequisitionNo").AsString().Nullable()
                  .WithColumn("RequestedBy").AsInt32().Nullable()
                  .WithColumn("RequestedDate").AsDateTime().Nullable()
                  .WithColumn("ApprovedBy").AsInt32().Nullable()
                  .WithColumn("ApprovedDate").AsDateTime().Nullable()
                  .WithColumn("Status").AsInt32().Nullable()
                  .WithColumn("ProgramID").AsInt32().Nullable();
            Create.Table("RegionalRequest").InSchema("EarlyWarning")
                  .WithColumn("RegionalRequestID").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("RequestDate").AsDateTime().NotNullable()
                   .WithColumn("ProgramID").AsInt32().NotNullable()
                   .WithColumn("Round").AsInt32().NotNullable()
                   .WithColumn("RegionID").AsInt32().NotNullable()
                  .WithColumn("RequestNumber").AsString(255).NotNullable()
                  .WithColumn("Year").AsInt32().NotNullable()
                  .WithColumn("Remark").AsString(255).Nullable();
            Create.Table("TransportRequisition").InSchema("Logistics")
               .WithColumn("TransportRequisitionID").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("TransportRequistionNo").AsString(50).NotNullable()
               .WithColumn("RequisitionID").AsInt32().NotNullable()
               .WithColumn("CommodityID").AsInt32().NotNullable()
               .WithColumn("Amount").AsDecimal().NotNullable()
               .WithColumn("RegionID").AsInt32().NotNullable()
               .WithColumn("ZoneID").AsInt32().NotNullable()
               .WithColumn("HubID").AsInt32().NotNullable()
               .WithColumn("SINumber").AsString(50).Nullable()
               .WithColumn("ProjectCode").AsInt32().Nullable()
               .WithColumn("Status").AsInt32().Nullable()
               .WithColumn("HubAllocatedBy").AsInt32().Nullable()
               .WithColumn("HubAllocationDate").AsDateTime().Nullable()
               .WithColumn("SIAllocatedBy").AsInt32().Nullable()
               .WithColumn("SIAllocationDate").AsDateTime().Nullable()
               .WithColumn("Remark").AsString(255).Nullable();




            Create.Table("ReliefRequisitionDetail").InSchema("EarlyWarning")
          .WithColumn("RequisitionDetailID").AsInt32().PrimaryKey().Identity()
          .WithColumn("RequisitionID").AsInt32().NotNullable()
          .WithColumn("CommodityID").AsInt32().NotNullable()
          .WithColumn("BenficiaryNo").AsInt32().NotNullable()
          .WithColumn("Amount").AsDecimal().NotNullable()
          .WithColumn("FDPID").AsInt32().NotNullable()
          .WithColumn("DonorID").AsInt32().Nullable();



            Create.Table("RegionalRequestDetail").InSchema("EarlyWarning")
               .WithColumn("RegionalRequestDetailID").AsInt32().PrimaryKey().Identity()
               .WithColumn("RegionalRequestID").AsInt32().NotNullable()
               .WithColumn("FDPID").AsInt32().NotNullable()
               .WithColumn("Grain").AsDecimal().Nullable()
               .WithColumn("Pulse").AsDecimal().Nullable()
               .WithColumn("Oil").AsDecimal().Nullable()
               .WithColumn("CSB").AsDecimal().Nullable()
               .WithColumn("Beneficiaries").AsInt32().NotNullable();


            Create.ForeignKey("FK_RegionalRequest_AdminUnit")
               .FromTable("RegionalRequest").InSchema("EarlyWarning").ForeignColumn("RegionID")
               .ToTable("AdminUnit").InSchema("dbo").PrimaryColumn("AdminUnitID");

            Create.ForeignKey("FK_RegionalRequest_Program")
                .FromTable("RegionalRequest").InSchema("EarlyWarning").ForeignColumn("ProgramID")
                .ToTable("Program").InSchema("dbo").PrimaryColumn("ProgramID");

            Create.ForeignKey("FK_RegionalRequestDetail_FDP")
              .FromTable("RegionalRequestDetail").InSchema("EarlyWarning").ForeignColumn("FDPID")
              .ToTable("FDP").PrimaryColumn("FDPID");


            Create.ForeignKey("FK_RegionalRequestDetail_RegionalRequest")
              .FromTable("RegionalRequestDetail").InSchema("EarlyWarning").ForeignColumn("RegionalRequestID")
              .ToTable("RegionalRequest").InSchema("EarlyWarning").PrimaryColumn("RegionalRequestID");



            Create.ForeignKey("FK_ReliefRequisition_AdminUnit")
             .FromTable("ReliefRequisition").InSchema("EarlyWarning").ForeignColumn("ZoneID")
             .ToTable("AdminUnit").InSchema("dbo").PrimaryColumn("AdminUnitID");

            /****** Object:  ForeignKey [FK_ReliefRequisition_AdminUnit    Script Date: 06/27/2013 11:37:40 ******/



            Create.ForeignKey("FK_ReliefRequisition_AdminUnit1")
             .FromTable("ReliefRequisition").InSchema("EarlyWarning").ForeignColumn("RegionID")
             .ToTable("AdminUnit").InSchema("dbo").PrimaryColumn("AdminUnitID");


            Create.ForeignKey("FK_ReliefRequisition_Program")
           .FromTable("ReliefRequisition").InSchema("EarlyWarning").ForeignColumn("ProgramID")
           .ToTable("Program").InSchema("dbo").PrimaryColumn("ProgramID");

            Create.ForeignKey("FK_ReliefRequisition_UserProfile")
        .FromTable("ReliefRequisition").InSchema("EarlyWarning").ForeignColumn("RequestedBy")
        .ToTable("UserProfile").InSchema("dbo").PrimaryColumn("UserProfileID");

            Create.ForeignKey("FK_ReliefRequisition_UserProfile1")
    .FromTable("ReliefRequisition").InSchema("EarlyWarning").ForeignColumn("ApprovedBy")
    .ToTable("UserProfile").InSchema("dbo").PrimaryColumn("UserProfileID");


            Create.ForeignKey("FK_ReliefRequisitionDetail_Commodity")
   .FromTable("ReliefRequisitionDetail").InSchema("EarlyWarning").ForeignColumn("CommodityID")
   .ToTable("Commodity").InSchema("dbo").PrimaryColumn("CommodityID");


            Create.ForeignKey("FK_ReliefRequisitionDetail_Donor")
           .FromTable("ReliefRequisitionDetail").InSchema("EarlyWarning").ForeignColumn("DonorID")
           .ToTable("Donor").InSchema("dbo").PrimaryColumn("DonorID");


            Create.ForeignKey("FK_ReliefRequisitionDetail_FDP")
           .FromTable("ReliefRequisitionDetail").InSchema("EarlyWarning").ForeignColumn("FDPID")
           .ToTable("FDP").InSchema("dbo").PrimaryColumn("FDPID");


            Create.ForeignKey("FK_ReliefRequisitionDetail_ReliefRequisition")
           .FromTable("ReliefRequisitionDetail").InSchema("EarlyWarning").ForeignColumn("RequisitionID")
           .ToTable("ReliefRequisition").InSchema("EarlyWarning").PrimaryColumn("RequisitionID");


            Create.ForeignKey("FK_TransportRequisition_AdminUnit")
                 .FromTable("TransportRequisition").InSchema("Logistics").ForeignColumn("RegionID")
                 .ToTable("AdminUnit").InSchema("dbo").PrimaryColumn("AdminUnitID");


            Create.ForeignKey("FK_TransportRequisition_AdminUnit1")
                 .FromTable("TransportRequisition").InSchema("Logistics").ForeignColumn("ZoneID")
                 .ToTable("AdminUnit").InSchema("dbo").PrimaryColumn("AdminUnitID");


            Create.ForeignKey("FK_TransportRequisition_Hub")
                 .FromTable("TransportRequisition").InSchema("Logistics").ForeignColumn("HubID")
                 .ToTable("Hub").InSchema("dbo").PrimaryColumn("HubID");

            Create.ForeignKey("FK_TransportRequisition_ProjectCode")
          .FromTable("TransportRequisition").InSchema("Logistics").ForeignColumn("ProjectCode")
          .ToTable("ProjectCode").InSchema("dbo").PrimaryColumn("ProjectCodeID");


            Create.ForeignKey("FK_TransportRequisition_ReliefRequisition")
                .FromTable("TransportRequisition").InSchema("Logistics").ForeignColumn("RequisitionID")
                .ToTable("ReliefRequisition").InSchema("EarlyWarning").PrimaryColumn("RequisitionID");


            Create.ForeignKey("FK_TransportRequisition_UserProfile")
                  .FromTable("TransportRequisition").InSchema("Logistics").ForeignColumn("HubAllocatedBy")
                  .ToTable("UserProfile").InSchema("dbo").PrimaryColumn("UserProfileID");

            Create.ForeignKey("FK_TransportRequisition_UserProfile1")
               .FromTable("TransportRequisition").InSchema("Logistics").ForeignColumn("SIAllocatedBy")
               .ToTable("UserProfile").InSchema("dbo").PrimaryColumn("UserProfileID");


        }

        public override void Down()
        {

            Delete.Table("ReliefRequisitionDetail").InSchema("EarlyWarning");
            Delete.Table("RegionalRequestDetail").InSchema("EarlyWarning");
            Delete.Table("ReliefRequisitionDetail").InSchema("EarlyWarning");
            Delete.Table("TransportRequisition").InSchema("Logistics");
            Delete.Table("RegionalRequest").InSchema("EarlyWarning");
            Delete.Table("ReliefRequisition").InSchema("EarlyWarning");
           
        }
    }
}