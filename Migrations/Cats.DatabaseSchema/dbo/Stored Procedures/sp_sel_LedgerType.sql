
CREATE PROCEDURE [sp_sel_LedgerType]
AS
  SELECT 
    [LedgerTypeID],
    [Name],
    [Direction]
  FROM 
    [dbo].[LedgerType]