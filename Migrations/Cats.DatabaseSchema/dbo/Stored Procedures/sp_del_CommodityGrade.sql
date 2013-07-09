

CREATE PROCEDURE [sp_del_CommodityGrade] (
  @CommodityGradeID int
)
AS
  DELETE FROM [dbo].[CommodityGrade]
  WHERE 
    ([CommodityGradeID] = @CommodityGradeID)