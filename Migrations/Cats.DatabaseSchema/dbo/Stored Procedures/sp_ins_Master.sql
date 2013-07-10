

CREATE PROCEDURE [sp_ins_Master] (
  @Name nvarchar(50),
  @SortOrder int
)
AS
  INSERT INTO [dbo].[Master] (
    [Name],
    [SortOrder]
  )
  VALUES (
    @Name,
    @SortOrder
  )