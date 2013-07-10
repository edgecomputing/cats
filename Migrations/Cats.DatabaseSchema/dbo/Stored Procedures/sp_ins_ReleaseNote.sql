

CREATE PROCEDURE [sp_ins_ReleaseNote] (
  @ReleaseName nvarchar(50),
  @BuildNumber int,
  @Date datetime,
  @Notes text,
  @Details text,
  @DBuildQuality int,
  @Comments text
)
AS
  INSERT INTO [dbo].[ReleaseNote] (
    [ReleaseName],
    [BuildNumber],
    [Date],
    [Notes],
    [Details],
    [DBuildQuality],
    [Comments]
  )
  VALUES (
    @ReleaseName,
    @BuildNumber,
    @Date,
    @Notes,
    @Details,
    @DBuildQuality,
    @Comments
  )