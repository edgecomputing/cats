

CREATE PROCEDURE [sp_upd_Role] (
  @RoleID int,
  @SortOrder int,
  @Name varchar(50),
  @Description varchar(50)
)
AS
  UPDATE [dbo].[Role] SET
    [SortOrder] = @SortOrder,
    [Name] = @Name,
    [Description] = @Description
  WHERE 
    ([RoleID] = @RoleID)