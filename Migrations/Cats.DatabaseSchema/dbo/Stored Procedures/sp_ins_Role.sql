

CREATE PROCEDURE [sp_ins_Role] (
  @SortOrder int,
  @Name varchar(50),
  @Description varchar(50)
)
AS
  INSERT INTO [dbo].[Role] (
    [SortOrder],
    [Name],
    [Description]
  )
  VALUES (
    @SortOrder,
    @Name,
    @Description
  )