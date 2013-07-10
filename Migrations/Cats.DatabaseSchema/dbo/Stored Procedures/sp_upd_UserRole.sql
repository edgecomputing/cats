

CREATE PROCEDURE [sp_upd_UserRole] (
  @UserRoleID int,
  @UserProfileID int,
  @RoleID int
)
AS
  UPDATE [dbo].[UserRole] SET
    [UserProfileID] = @UserProfileID,
    [RoleID] = @RoleID
  WHERE 
    ([UserRoleID] = @UserRoleID)