

CREATE PROCEDURE [sp_upd_UserHub] (
  @UserHubID int,
  @UserProfileID int,
  @HubID int,
  @IsDefault char
)
AS
  UPDATE [dbo].[UserHub] SET
    [UserProfileID] = @UserProfileID,
    [HubID] = @HubID,
    [IsDefault] = @IsDefault
  WHERE 
    ([UserHubID] = @UserHubID)