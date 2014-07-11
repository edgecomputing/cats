
CREATE PROCEDURE [sp_sel_Role]
AS
  SELECT 
    [RoleID],
    [SortOrder],
    [Name],
    [Description]
  FROM 
    [dbo].[Role]