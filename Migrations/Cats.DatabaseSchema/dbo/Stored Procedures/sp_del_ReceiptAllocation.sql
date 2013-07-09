

CREATE PROCEDURE [sp_del_ReceiptAllocation] (
  @PartitionID int,
  @ReceiptAllocationID int
)
AS
  DELETE FROM [dbo].[ReceiptAllocation]
  WHERE 
    ([PartitionID] = @PartitionID) AND
    ([ReceiptAllocationID] = @ReceiptAllocationID)