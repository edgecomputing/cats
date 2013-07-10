
CREATE PROCEDURE [sp_sel_Store]
AS
  SELECT 
    [StoreID],
    [Name],
    [HubID],
    [IsTemporary],
    [IsActive],
    [StackCount],
    [StoreManName]
  FROM 
    [dbo].[Store]