

CREATE PROCEDURE [sp_ins_Store] (
  @Name nvarchar(50),
  @HubID int,
  @IsTemporary bit,
  @IsActive bit,
  @StackCount int,
  @StoreManName nvarchar(50)
)
AS
  INSERT INTO [dbo].[Store] (
    [Name],
    [HubID],
    [IsTemporary],
    [IsActive],
    [StackCount],
    [StoreManName]
  )
  VALUES (
    @Name,
    @HubID,
    @IsTemporary,
    @IsActive,
    @StackCount,
    @StoreManName
  )