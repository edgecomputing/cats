

CREATE PROCEDURE [sp_ins_ForgetPasswordRequest] (
  @UserProfileID int,
  @GeneratedDate datetime,
  @ExpieryDate datetime,
  @Completed bit,
  @RequestKey nvarchar(50)
)
AS
  INSERT INTO [dbo].[ForgetPasswordRequest] (
    [UserProfileID],
    [GeneratedDate],
    [ExpieryDate],
    [Completed],
    [RequestKey]
  )
  VALUES (
    @UserProfileID,
    @GeneratedDate,
    @ExpieryDate,
    @Completed,
    @RequestKey
  )