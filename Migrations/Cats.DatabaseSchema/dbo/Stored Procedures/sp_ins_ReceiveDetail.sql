

CREATE PROCEDURE [sp_ins_ReceiveDetail] (
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
  INSERT INTO [dbo].[ReceiveDetail] (
    [PartitionID],
    [ReceiveID],
    [TransactionGroupID],
    [CommodityID],
    [SentQuantityInUnit],
    [UnitID],
    [SentQuantityInMT],
    [Description]
  )
  VALUES (
    @PartitionID,
    @ReceiveID,
    @TransactionGroupID,
    @CommodityID,
    @SentQuantityInUnit,
    @UnitID,
    @SentQuantityInMT,
    @Description
  )