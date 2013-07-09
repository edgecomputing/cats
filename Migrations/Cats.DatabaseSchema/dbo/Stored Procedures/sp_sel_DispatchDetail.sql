
CREATE PROCEDURE [sp_sel_DispatchDetail]
AS
  SELECT 
    [DispatchDetailID],
    [PartitionID],
    [TransactionGroupID],
    [DispatchID],
    [CommodityID],
    [RequestedQunatityInUnit],
    [UnitID],
    [RequestedQuantityInMT],
    [Description]
  FROM 
    [dbo].[DispatchDetail]