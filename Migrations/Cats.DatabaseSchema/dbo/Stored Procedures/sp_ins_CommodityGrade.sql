

CREATE PROCEDURE [sp_ins_CommodityGrade] (
  @Name nvarchar(50),
  @Description nvarchar(50),
  @SortOrder int
)
AS
  INSERT INTO [dbo].[CommodityGrade] (
    [Name],
    [Description],
    [SortOrder]
  )
  VALUES (
    @Name,
    @Description,
    @SortOrder
  )