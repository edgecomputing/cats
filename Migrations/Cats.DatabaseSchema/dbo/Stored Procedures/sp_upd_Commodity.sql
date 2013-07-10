

CREATE PROCEDURE [sp_upd_Commodity] (
  @CommodityID int,
  @Name nvarchar(50),
  @LongName nvarchar(500),
  @CommodityTypeID int,
  @ParentID int
)
AS
  UPDATE [dbo].[Commodity] SET
    [Name] = @Name,
    [LongName] = @LongName,
    [CommodityTypeID] = @CommodityTypeID,
    [ParentID] = @ParentID
  WHERE 
    ([CommodityID] = @CommodityID)