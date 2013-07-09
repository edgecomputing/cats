

CREATE PROCEDURE [sp_upd_GiftCertificateDetail] (
  @GiftCertificateDetailID int,
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
  UPDATE [dbo].[GiftCertificateDetail] SET
    [PartitionID] = @PartitionID,
    [TransactionGroupID] = @TransactionGroupID,
    [GiftCertificateID] = @GiftCertificateID,
    [CommodityID] = @CommodityID,
    [WeightInMT] = @WeightInMT,
    [BillOfLaoading] = @BillOfLaoading,
    [AccountNumber] = @AccountNumber,
    [EstimatedPrice] = @EstimatedPrice,
    [EstimatedTax] = @EstimatedTax,
    [YearPurchased] = @YearPurchased,
    [DFundSourceID] = @DFundSourceID,
    [DCurrencyID] = @DCurrencyID,
    [DBudgetTypeID] = @DBudgetTypeID
  WHERE 
    ([GiftCertificateDetailID] = @GiftCertificateDetailID)