
CREATE PROCEDURE [sp_sel_TransactionGroup]
AS
  SELECT 
    [TransactionGroupID],
    [PartitionID]
  FROM 
    [dbo].[TransactionGroup]