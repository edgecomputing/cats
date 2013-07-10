

CREATE PROCEDURE [sp_upd_ReleaseNote] (
  @ReleaseNoteID int,
  @ReleaseName nvarchar(50),
  @BuildNumber int,
  @Date datetime,
  @Notes text,
  @Details text,
  @DBuildQuality int,
  @Comments text
)
AS
  UPDATE [dbo].[ReleaseNote] SET
    [ReleaseName] = @ReleaseName,
    [BuildNumber] = @BuildNumber,
    [Date] = @Date,
    [Notes] = @Notes,
    [Details] = @Details,
    [DBuildQuality] = @DBuildQuality,
    [Comments] = @Comments
  WHERE 
    ([ReleaseNoteID] = @ReleaseNoteID)