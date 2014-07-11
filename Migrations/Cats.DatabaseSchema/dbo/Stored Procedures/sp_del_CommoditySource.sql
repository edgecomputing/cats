

CREATE PROCEDURE [sp_del_CommoditySource] (
  @CommoditySourceID int
)
AS
  DELETE FROM [dbo].[CommoditySource]
  WHERE 
    ([CommoditySourceID] = @CommoditySourceID)