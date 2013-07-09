

CREATE PROCEDURE [sp_ins_CommoditySource] (
  @Name nvarchar(50)
)
AS
  INSERT INTO [dbo].[CommoditySource] (
    [Name]
  )
  VALUES (
    @Name
  )