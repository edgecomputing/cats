

CREATE PROCEDURE [sp_upd_DispatchAllocation] (
  @PartitionID int,
  @DispatchAllocationID int,
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
  UPDATE [dbo].[DispatchAllocation] SET
    [TransactionGroupID] = @TransactionGroupID,
    [HubID] = @HubID,
    [Year] = @Year,
    [Month] = @Month,
    [Round] = @Round,
    [DonorID] = @DonorID,
    [ProgramID] = @ProgramID,
    [CommodityID] = @CommodityID,
    [RequisitionNo] = @RequisitionNo,
    [BidRefNo] = @BidRefNo,
    [Beneficiery] = @Beneficiery,
    [Amount] = @Amount,
    [Unit] = @Unit,
    [TransporterID] = @TransporterID,
    [FDPID] = @FDPID
  WHERE 
    ([PartitionID] = @PartitionID) AND
    ([DispatchAllocationID] = @DispatchAllocationID)