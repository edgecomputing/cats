
CREATE PROCEDURE [sp_sel_Transaction]
AS
  SELECT 
    [PartitionID],
    [TransactionID],
    [TransactionGroupID],
    [LedgerID],
    [HubOwnerID],
    [AccountID],
    [HubID],
    [StoreID],
    [Stack],
    [ProjectCodeID],
    [ShippingInstructionID],
    [ProgramID],
    [ParentCommodityID],
    [CommodityID],
    [CommodityGradeID],
    [QuantityInMT],
    [QuantityInUnit],
    [UnitID],
    [TransactionDate]
  FROM 
    [dbo].[Transaction]