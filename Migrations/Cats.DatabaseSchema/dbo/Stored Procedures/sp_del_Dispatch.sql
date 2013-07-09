

CREATE PROCEDURE [sp_del_Dispatch] (
  @DispatchID int,
  @PartitionID int
)
AS
  DELETE FROM [dbo].[Dispatch]
  WHERE 
    ([DispatchID] = @DispatchID) AND
    ([PartitionID] = @PartitionID)