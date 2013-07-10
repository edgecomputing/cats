

CREATE PROCEDURE [sp_del_DispatchDetail] (
  @DispatchDetailID int,
  @PartitionID int
)
AS
  DELETE FROM [dbo].[DispatchDetail]
  WHERE 
    ([DispatchDetailID] = @DispatchDetailID) AND
    ([PartitionID] = @PartitionID)