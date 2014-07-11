
CREATE PROCEDURE [sp_sel_Ledger]
AS
  SELECT 
    [LedgerID],
    [Name],
    [LedgerTypeID]
  FROM 
    [dbo].[Ledger]