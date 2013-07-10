

CREATE PROCEDURE [sp_del_HubOwner] (
  @HubOwnerID int
)
AS
  DELETE FROM [dbo].[HubOwner]
  WHERE 
    ([HubOwnerID] = @HubOwnerID)