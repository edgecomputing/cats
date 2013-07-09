
CREATE PROCEDURE [sp_sel_AdminUnit]
AS
  SELECT 
    [AdminUnitID],
    [Name],
    [AdminUnitTypeID],
    [ParentID]
  FROM 
    [dbo].[AdminUnit]