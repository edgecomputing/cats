
CREATE PROCEDURE [sp_sel_AdminUnitType]
AS
  SELECT 
    [AdminUnitTypeID],
    [Name],
    [SortOrder]
  FROM 
    [dbo].[AdminUnitType]