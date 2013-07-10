

CREATE PROCEDURE [sp_del_LedgerType] (
  @LedgerTypeID int
)
AS
  DELETE FROM [dbo].[LedgerType]
  WHERE 
    ([LedgerTypeID] = @LedgerTypeID)