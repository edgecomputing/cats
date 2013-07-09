CREATE PROCEDURE [sp_sel_Account]
AS
  SELECT 
    [AccountID],
    [EntityType],
    [EntityID]
  FROM 
    [dbo].[Account]