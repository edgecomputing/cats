

CREATE PROCEDURE [sp_upd_HubOwner] (
  @HubOwnerID int,
  @Name nvarchar(50),
  @LongName nvarchar(500)
)
AS
  UPDATE [dbo].[HubOwner] SET
    [Name] = @Name,
    [LongName] = @LongName
  WHERE 
    ([HubOwnerID] = @HubOwnerID)