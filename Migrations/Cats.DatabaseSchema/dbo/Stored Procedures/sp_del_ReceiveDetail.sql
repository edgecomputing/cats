

CREATE PROCEDURE [sp_del_ReceiveDetail] (
  @ReceiveDetailID int,
  @PartitionID int
)
AS
  DELETE FROM [dbo].[ReceiveDetail]
  WHERE 
    ([ReceiveDetailID] = @ReceiveDetailID) AND
    ([PartitionID] = @PartitionID)