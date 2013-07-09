

CREATE PROCEDURE [sp_del_Transaction] (
  @PartitionID int,
  @TransactionID int
)
AS
  DELETE FROM [dbo].[Transaction]
  WHERE 
    ([PartitionID] = @PartitionID) AND
    ([TransactionID] = @TransactionID)