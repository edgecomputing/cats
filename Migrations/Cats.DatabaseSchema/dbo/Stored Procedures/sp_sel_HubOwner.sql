
CREATE PROCEDURE [sp_sel_HubOwner]
AS
  SELECT 
    [HubOwnerID],
    [Name],
    [LongName]
  FROM 
    [dbo].[HubOwner]