

CREATE PROCEDURE [sp_del_SessionAttempts] (
  @SessionAttemptID int
)
AS
  DELETE FROM [dbo].[SessionAttempts]
  WHERE 
    ([SessionAttemptID] = @SessionAttemptID)