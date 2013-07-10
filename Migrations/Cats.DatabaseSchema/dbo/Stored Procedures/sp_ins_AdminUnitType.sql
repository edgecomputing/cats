

CREATE PROCEDURE [sp_ins_AdminUnitType] (
  @Name nvarchar(50),
  @SortOrder int
)
AS
  INSERT INTO [dbo].[AdminUnitType] (
    [Name],
    [SortOrder]
  )
  VALUES (
    @Name,
    @SortOrder
  )