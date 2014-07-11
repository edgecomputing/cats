

CREATE PROCEDURE [sp_ins_CommodityType] (
  @Name nvarchar(50)
)
AS
  INSERT INTO [dbo].[CommodityType] (
    [Name]
  )
  VALUES (
    @Name
  )