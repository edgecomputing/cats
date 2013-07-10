

CREATE PROCEDURE [sp_upd_CommodityGrade] (
  @CommodityGradeID int,
  @Name nvarchar(50),
  @Description nvarchar(50),
  @SortOrder int
)
AS
  UPDATE [dbo].[CommodityGrade] SET
    [Name] = @Name,
    [Description] = @Description,
    [SortOrder] = @SortOrder
  WHERE 
    ([CommodityGradeID] = @CommodityGradeID)