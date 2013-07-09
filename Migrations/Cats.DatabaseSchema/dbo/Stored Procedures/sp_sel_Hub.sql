
CREATE PROCEDURE [sp_sel_Hub]
AS
  SELECT 
    [HubID],
    [Name],
    [HubOwnerID]
  FROM 
    [dbo].[Hub]