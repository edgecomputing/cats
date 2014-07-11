

CREATE PROCEDURE [sp_upd_ReceiptAllocation] (
  @PartitionID int,
  @ReceiptAllocationID int,
  @TransactionGroupID int,
  @ETA date,
  @ProjectNumber nvarchar(50),
  @GiftCertificateDetailID int,
  @CommodityID int,
  @SINumber nvarchar(50),
  @QuantityInMT decimal(18,3),
  @HubID int,
  @DonorID int,
  @ProgramID int,
  @CommoditySourceID int
)
AS
  UPDATE [dbo].[ReceiptAllocation] SET
    [TransactionGroupID] = @TransactionGroupID,
    [ETA] = @ETA,
    [ProjectNumber] = @ProjectNumber,
    [GiftCertificateDetailID] = @GiftCertificateDetailID,
    [CommodityID] = @CommodityID,
    [SINumber] = @SINumber,
    [QuantityInMT] = @QuantityInMT,
    [HubID] = @HubID,
    [DonorID] = @DonorID,
    [ProgramID] = @ProgramID,
    [CommoditySourceID] = @CommoditySourceID
  WHERE 
    ([PartitionID] = @PartitionID) AND
    ([ReceiptAllocationID] = @ReceiptAllocationID)