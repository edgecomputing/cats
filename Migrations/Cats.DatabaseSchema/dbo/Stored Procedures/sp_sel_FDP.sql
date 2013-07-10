
CREATE PROCEDURE [sp_sel_FDP]
AS
  SELECT 
    [FDPID],
    [Name],
    [AdminUnitID]
  FROM 
    [dbo].[FDP]