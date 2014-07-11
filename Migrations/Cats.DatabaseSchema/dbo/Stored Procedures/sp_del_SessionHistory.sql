

CREATE PROCEDURE [sp_del_SessionHistory] (
  @SessionHistoryID int
)
AS
  DELETE FROM [dbo].[SessionHistory]
  WHERE 
    ([SessionHistoryID] = @SessionHistoryID)