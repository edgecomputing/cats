

CREATE PROCEDURE [sp_ins_Ledger] (
  @Name nvarchar(50),
  @LedgerTypeID int
)
AS
  INSERT INTO [dbo].[Ledger] (
    [Name],
    [LedgerTypeID]
  )
  VALUES (
    @Name,
    @LedgerTypeID
  )