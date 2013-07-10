
CREATE PROCEDURE [sp_sel_Period]
AS
  SELECT 
    [PeriodID],
    [Year],
    [Month]
  FROM 
    [dbo].[Period]