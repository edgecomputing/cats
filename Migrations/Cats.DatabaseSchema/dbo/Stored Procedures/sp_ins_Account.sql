

CREATE PROCEDURE [sp_ins_Account] (
  @EntityType nvarchar(50),
  @EntityID int
)
AS
  INSERT INTO [dbo].[Account] (
    [EntityType],
    [EntityID]
  )
  VALUES (
    @EntityType,
    @EntityID
  )