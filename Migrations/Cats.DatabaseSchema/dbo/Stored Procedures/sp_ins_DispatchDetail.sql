

CREATE PROCEDURE [sp_ins_DispatchDetail] (
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
  INSERT INTO [dbo].[DispatchDetail] (
    [PartitionID],
    [TransactionGroupID],
    [DispatchID],
    [CommodityID],
    [RequestedQunatityInUnit],
    [UnitID],
    [RequestedQuantityInMT],
    [Description]
  )
  VALUES (
    @PartitionID,
    @TransactionGroupID,
    @DispatchID,
    @CommodityID,
    @RequestedQunatityInUnit,
    @UnitID,
    @RequestedQuantityInMT,
    @Description
  )