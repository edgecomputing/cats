

CREATE PROCEDURE [sp_upd_Period] (
  @PeriodID int,
  @Year int,
  @Month int
)
AS
  UPDATE [dbo].[Period] SET
    [Year] = @Year,
    [Month] = @Month
  WHERE 
    ([PeriodID] = @PeriodID)