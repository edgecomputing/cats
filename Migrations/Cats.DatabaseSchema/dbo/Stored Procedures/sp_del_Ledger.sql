

CREATE PROCEDURE [sp_del_Ledger] (
  @LedgerID int
)
AS
  DELETE FROM [dbo].[Ledger]
  WHERE 
    ([LedgerID] = @LedgerID)