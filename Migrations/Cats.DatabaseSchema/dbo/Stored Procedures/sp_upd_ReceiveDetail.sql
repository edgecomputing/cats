

CREATE PROCEDURE [sp_upd_ReceiveDetail] (
  @ReceiveDetailID int,
  @PartitionID int,
  @ReceiveID int,
  @TransactionGroupID int,
  @CommodityID int,
  @SentQuantityInUnit decimal(18,3),
  @UnitID int,
  @SentQuantityInMT decimal(18,3),
  @Description nvarchar(500)
)
AS
  UPDATE [dbo].[ReceiveDetail] SET
    [ReceiveID] = @ReceiveID,
    [TransactionGroupID] = @TransactionGroupID,
    [CommodityID] = @CommodityID,
    [SentQuantityInUnit] = @SentQuantityInUnit,
    [UnitID] = @UnitID,
    [SentQuantityInMT] = @SentQuantityInMT,
    [Description] = @Description
  WHERE 
    ([ReceiveDetailID] = @ReceiveDetailID) AND
    ([PartitionID] = @PartitionID)