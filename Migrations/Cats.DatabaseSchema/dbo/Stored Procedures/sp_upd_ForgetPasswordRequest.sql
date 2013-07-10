

CREATE PROCEDURE [sp_upd_ForgetPasswordRequest] (
  @ForgetPasswordRequestID int,
  @UserProfileID int,
  @GeneratedDate datetime,
  @ExpieryDate datetime,
  @Completed bit,
  @RequestKey nvarchar(50)
)
AS
  UPDATE [dbo].[ForgetPasswordRequest] SET
    [UserProfileID] = @UserProfileID,
    [GeneratedDate] = @GeneratedDate,
    [ExpieryDate] = @ExpieryDate,
    [Completed] = @Completed,
    [RequestKey] = @RequestKey
  WHERE 
    ([ForgetPasswordRequestID] = @ForgetPasswordRequestID)