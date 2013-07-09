
CREATE PROCEDURE [sp_sel_Commodity]
AS
  SELECT 
    [CommodityID],
    [Name],
    [LongName],
    [CommodityTypeID],
    [ParentID]
  FROM 
    [dbo].[Commodity]