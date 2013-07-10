
CREATE PROCEDURE [sp_sel_ReleaseNote]
AS
  SELECT 
    [ReleaseNoteID],
    [ReleaseName],
    [BuildNumber],
    [Date],
    [Notes],
    [Details],
    [DBuildQuality],
    [Comments]
  FROM 
    [dbo].[ReleaseNote]