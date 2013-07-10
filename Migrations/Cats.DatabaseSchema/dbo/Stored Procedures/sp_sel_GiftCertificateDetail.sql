
CREATE PROCEDURE [sp_sel_GiftCertificateDetail]
AS
  SELECT 
    [GiftCertificateDetailID],
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
  FROM 
    [dbo].[GiftCertificateDetail]