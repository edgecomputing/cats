

CREATE PROCEDURE [sp_del_Contact] (
  @ContactID int
)
AS
  DELETE FROM [dbo].[Contact]
  WHERE 
    ([ContactID] = @ContactID)