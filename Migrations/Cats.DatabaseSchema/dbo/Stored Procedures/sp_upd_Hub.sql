

CREATE PROCEDURE [sp_upd_Hub] (
  @HubID int,
  @Name nvarchar(50),
  @HubOwnerID int
)
AS
  UPDATE [dbo].[Hub] SET
    [Name] = @Name,
    [HubOwnerID] = @HubOwnerID
  WHERE 
    ([HubID] = @HubID)