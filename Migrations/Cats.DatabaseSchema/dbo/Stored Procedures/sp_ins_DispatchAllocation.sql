

CREATE PROCEDURE [sp_ins_DispatchAllocation] (
  @PartitionID int,
  @TransactionGroupID int,
  @HubID int,
  @Year int,
  @Month int,
  @Round int,
  @DonorID int,
  @ProgramID int,
  @CommodityID int,
  @RequisitionNo nvarchar(50),
  @BidRefNo nvarchar(50),
  @Beneficiery int,
  @Amount decimal(18,3),
  @Unit int,
  @TransporterID int,
  @FDPID int
)
AS
  INSERT INTO [dbo].[DispatchAllocation] (
    [PartitionID],
    [TransactionGroupID],
    [HubID],
    [Year],
    [Month],
    [Round],
    [DonorID],
    [ProgramID],
    [CommodityID],
    [RequisitionNo],
    [BidRefNo],
    [Beneficiery],
    [Amount],
    [Unit],
    [TransporterID],
    [FDPID]
  )
  VALUES (
    @PartitionID,
    @TransactionGroupID,
    @HubID,
    @Year,
    @Month,
    @Round,
    @DonorID,
    @ProgramID,
    @CommodityID,
    @RequisitionNo,
    @BidRefNo,
    @Beneficiery,
    @Amount,
    @Unit,
    @TransporterID,
    @FDPID
  )