
CREATE PROCEDURE [sp_sel_UserRole]
AS
  SELECT 
    [UserRoleID],
    [UserProfileID],
    [RoleID]
  FROM 
    [dbo].[UserRole]