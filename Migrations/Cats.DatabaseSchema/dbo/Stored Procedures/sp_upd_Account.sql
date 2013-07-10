

CREATE PROCEDURE [sp_upd_Account] (
  @AccountID int,
  @EntityType nvarchar(50),
  @EntityID int
)
AS
  UPDATE [dbo].[Account] SET
    [EntityType] = @EntityType,
    [EntityID] = @EntityID
  WHERE 
    ([AccountID] = @AccountID)