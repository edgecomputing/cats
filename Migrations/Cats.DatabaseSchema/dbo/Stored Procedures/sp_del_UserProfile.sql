

CREATE PROCEDURE [sp_del_UserProfile] (
  @UserProfileID int
)
AS
  DELETE FROM [dbo].[UserProfile]
  WHERE 
    ([UserProfileID] = @UserProfileID)