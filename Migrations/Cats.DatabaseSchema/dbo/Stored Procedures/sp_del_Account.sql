

CREATE PROCEDURE [sp_del_Account] (
  @AccountID int
)
AS
  DELETE FROM [dbo].[Account]
  WHERE 
    ([AccountID] = @AccountID)