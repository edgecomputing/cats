

CREATE PROCEDURE [sp_del_DispatchAllocation] (
  @PartitionID int,
  @DispatchAllocationID int
)
AS
  DELETE FROM [dbo].[DispatchAllocation]
  WHERE 
    ([PartitionID] = @PartitionID) AND
    ([DispatchAllocationID] = @DispatchAllocationID)