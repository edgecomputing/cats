
CREATE PROCEDURE [sp_sel_CommodityGrade]
AS
  SELECT 
    [CommodityGradeID],
    [Name],
    [Description],
    [SortOrder]
  FROM 
    [dbo].[CommodityGrade]