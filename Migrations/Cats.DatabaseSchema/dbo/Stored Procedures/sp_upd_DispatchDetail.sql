

CREATE PROCEDURE [sp_upd_DispatchDetail] (
  @DispatchDetailID int,
  @PartitionID int,
  @TransactionGroupID int,
  @DispatchID int,
  @CommodityID int,
  @RequestedQunatityInUnit decimal(18,3),
  @UnitID int,
  @RequestedQuantityInMT decimal(18,3),
  @Description nvarchar(500)
)
AS
  UPDATE [dbo].[DispatchDetail] SET
    [TransactionGroupID] = @TransactionGroupID,
    [DispatchID] = @DispatchID,
    [CommodityID] = @CommodityID,
    [RequestedQunatityInUnit] = @RequestedQunatityInUnit,
    [UnitID] = @UnitID,
    [RequestedQuantityInMT] = @RequestedQuantityInMT,
    [Description] = @Description
  WHERE 
    ([DispatchDetailID] = @DispatchDetailID) AND
    ([PartitionID] = @PartitionID)