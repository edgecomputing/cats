

CREATE PROCEDURE [sp_upd_LedgerType] (
  @LedgerTypeID int,
  @Name nvarchar(50),
  @Direction char
)
AS
  UPDATE [dbo].[LedgerType] SET
    [Name] = @Name,
    [Direction] = @Direction
  WHERE 
    ([LedgerTypeID] = @LedgerTypeID)