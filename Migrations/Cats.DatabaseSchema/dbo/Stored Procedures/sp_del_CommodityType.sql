

CREATE PROCEDURE [sp_del_CommodityType] (
  @CommodityTypeID int
)
AS
  DELETE FROM [dbo].[CommodityType]
  WHERE 
    ([CommodityTypeID] = @CommodityTypeID)