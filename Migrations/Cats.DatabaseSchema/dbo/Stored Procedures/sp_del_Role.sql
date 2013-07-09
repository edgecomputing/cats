

CREATE PROCEDURE [sp_del_Role] (
  @RoleID int
)
AS
  DELETE FROM [dbo].[Role]
  WHERE 
    ([RoleID] = @RoleID)