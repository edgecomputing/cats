

CREATE PROCEDURE [sp_ins_TransactionGroup] (
  @PartitionID int
)
AS
  INSERT INTO [dbo].[TransactionGroup] (
    [PartitionID]
  )
  VALUES (
    @PartitionID
  )