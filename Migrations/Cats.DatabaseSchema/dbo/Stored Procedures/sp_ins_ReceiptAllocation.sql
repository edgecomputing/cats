

CREATE PROCEDURE [sp_ins_ReceiptAllocation] (
  @PartitionID int,
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
  INSERT INTO [dbo].[ReceiptAllocation] (
    [PartitionID],
    [TransactionGroupID],
    [ETA],
    [ProjectNumber],
    [GiftCertificateDetailID],
    [CommodityID],
    [SINumber],
    [QuantityInMT],
    [HubID],
    [DonorID],
    [ProgramID],
    [CommoditySourceID]
  )
  VALUES (
    @PartitionID,
    @TransactionGroupID,
    @ETA,
    @ProjectNumber,
    @GiftCertificateDetailID,
    @CommodityID,
    @SINumber,
    @QuantityInMT,
    @HubID,
    @DonorID,
    @ProgramID,
    @CommoditySourceID
  )