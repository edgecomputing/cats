
CREATE PROCEDURE [sp_sel_CommodityType]
AS
  SELECT 
    [CommodityTypeID],
    [Name]
  FROM 
    [dbo].[CommodityType]