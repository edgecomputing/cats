

CREATE PROCEDURE [sp_del_Hub] (
  @HubID int
)
AS
  DELETE FROM [dbo].[Hub]
  WHERE 
    ([HubID] = @HubID)