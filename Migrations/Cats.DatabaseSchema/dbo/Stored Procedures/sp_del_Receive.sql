

CREATE PROCEDURE [sp_del_Receive] (
  @ReceiveID int,
  @PartitionID int
)
AS
  DELETE FROM [dbo].[Receive]
  WHERE 
    ([ReceiveID] = @ReceiveID) AND
    ([PartitionID] = @PartitionID)