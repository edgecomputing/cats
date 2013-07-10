

CREATE PROCEDURE [sp_del_ReleaseNote] (
  @ReleaseNoteID int
)
AS
  DELETE FROM [dbo].[ReleaseNote]
  WHERE 
    ([ReleaseNoteID] = @ReleaseNoteID)