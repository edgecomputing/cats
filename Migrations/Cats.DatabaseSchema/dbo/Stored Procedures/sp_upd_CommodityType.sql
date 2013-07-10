

CREATE PROCEDURE [sp_upd_CommodityType] (
  @CommodityTypeID int,
  @Name nvarchar(50)
)
AS
  UPDATE [dbo].[CommodityType] SET
    [Name] = @Name
  WHERE 
    ([CommodityTypeID] = @CommodityTypeID)