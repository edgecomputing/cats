

CREATE PROCEDURE [sp_upd_Store] (
  @StoreID int,
  @Name nvarchar(50),
  @HubID int,
  @IsTemporary bit,
  @IsActive bit,
  @StackCount int,
  @StoreManName nvarchar(50)
)
AS
  UPDATE [dbo].[Store] SET
    [Name] = @Name,
    [HubID] = @HubID,
    [IsTemporary] = @IsTemporary,
    [IsActive] = @IsActive,
    [StackCount] = @StackCount,
    [StoreManName] = @StoreManName
  WHERE 
    ([StoreID] = @StoreID)