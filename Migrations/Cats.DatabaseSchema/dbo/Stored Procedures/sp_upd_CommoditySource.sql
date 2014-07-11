

CREATE PROCEDURE [sp_upd_CommoditySource] (
  @CommoditySourceID int,
  @Name nvarchar(50)
)
AS
  UPDATE [dbo].[CommoditySource] SET
    [Name] = @Name
  WHERE 
    ([CommoditySourceID] = @CommoditySourceID)