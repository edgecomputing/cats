

CREATE PROCEDURE [sp_del_Commodity] (
  @CommodityID int
)
AS
  DELETE FROM [dbo].[Commodity]
  WHERE 
    ([CommodityID] = @CommodityID)