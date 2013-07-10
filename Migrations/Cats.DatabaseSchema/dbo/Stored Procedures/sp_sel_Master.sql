
CREATE PROCEDURE [sp_sel_Master]
AS
  SELECT 
    [MasterID],
    [Name],
    [SortOrder]
  FROM 
    [dbo].[Master]