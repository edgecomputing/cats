
CREATE PROCEDURE [sp_sel_ReceiveDetail]
AS
  SELECT 
    [ReceiveDetailID],
    [PartitionID],
    [ReceiveID],
    [TransactionGroupID],
    [CommodityID],
    [SentQuantityInUnit],
    [UnitID],
    [SentQuantityInMT],
    [Description]
  FROM 
    [dbo].[ReceiveDetail]