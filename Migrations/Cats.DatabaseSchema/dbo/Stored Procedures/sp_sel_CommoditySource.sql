
CREATE PROCEDURE [sp_sel_CommoditySource]
AS
  SELECT 
    [CommoditySourceID],
    [Name]
  FROM 
    [dbo].[CommoditySource]