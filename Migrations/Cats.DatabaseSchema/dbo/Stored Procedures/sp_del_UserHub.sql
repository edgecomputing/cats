

CREATE PROCEDURE [sp_del_UserHub] (
  @UserHubID int
)
AS
  DELETE FROM [dbo].[UserHub]
  WHERE 
    ([UserHubID] = @UserHubID)