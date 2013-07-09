

CREATE PROCEDURE [sp_upd_Ledger] (
  @LedgerID int,
  @Name nvarchar(50),
  @LedgerTypeID int
)
AS
  UPDATE [dbo].[Ledger] SET
    [Name] = @Name,
    [LedgerTypeID] = @LedgerTypeID
  WHERE 
    ([LedgerID] = @LedgerID)