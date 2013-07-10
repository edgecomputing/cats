

CREATE PROCEDURE [sp_del_TransactionGroup] (
  @TransactionGroupID int,
  @PartitionID int
)
AS
  DELETE FROM [dbo].[TransactionGroup]
  WHERE 
    ([TransactionGroupID] = @TransactionGroupID) AND
    ([PartitionID] = @PartitionID)