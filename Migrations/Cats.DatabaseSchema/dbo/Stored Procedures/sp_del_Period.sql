

CREATE PROCEDURE [sp_del_Period] (
  @PeriodID int
)
AS
  DELETE FROM [dbo].[Period]
  WHERE 
    ([PeriodID] = @PeriodID)