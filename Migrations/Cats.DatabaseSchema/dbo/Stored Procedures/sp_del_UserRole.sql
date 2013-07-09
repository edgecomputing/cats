

CREATE PROCEDURE [sp_del_UserRole] (
  @UserRoleID int
)
AS
  DELETE FROM [dbo].[UserRole]
  WHERE 
    ([UserRoleID] = @UserRoleID)