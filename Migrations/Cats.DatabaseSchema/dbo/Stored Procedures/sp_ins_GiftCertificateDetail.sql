

CREATE PROCEDURE [sp_ins_GiftCertificateDetail] (
  @PartitionID int,
  @TransactionGroupID int,
  @GiftCertificateID int,
  @CommodityID int,
  @WeightInMT decimal(18,3),
  @BillOfLaoading nvarchar(50),
  @AccountNumber int,
  @EstimatedPrice decimal(18,2),
  @EstimatedTax decimal(18,2),
  @YearPurchased int,
  @DFundSourceID int,
  @DCurrencyID int,
  @DBudgetTypeID int
)
AS
  INSERT INTO [dbo].[GiftCertificateDetail] (
    [PartitionID],
    [TransactionGroupID],
    [GiftCertificateID],
    [CommodityID],
    [WeightInMT],
    [BillOfLaoading],
    [AccountNumber],
    [EstimatedPrice],
    [EstimatedTax],
    [YearPurchased],
    [DFundSourceID],
    [DCurrencyID],
    [DBudgetTypeID]
  )
  VALUES (
    @PartitionID,
    @TransactionGroupID,
    @GiftCertificateID,
    @CommodityID,
    @WeightInMT,
    @BillOfLaoading,
    @AccountNumber,
    @EstimatedPrice,
    @EstimatedTax,
    @YearPurchased,
    @DFundSourceID,
    @DCurrencyID,
    @DBudgetTypeID
  )