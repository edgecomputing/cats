

CREATE PROCEDURE [sp_ins_Commodity] (
  @Name nvarchar(50),
  @LongName nvarchar(500),
  @CommodityTypeID int,
  @ParentID int
)
AS
  INSERT INTO [dbo].[Commodity] (
    [Name],
    [LongName],
    [CommodityTypeID],
    [ParentID]
  )
  VALUES (
    @Name,
    @LongName,
    @CommodityTypeID,
    @ParentID
  )