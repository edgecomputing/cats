

CREATE PROCEDURE [sp_del_ForgetPasswordRequest] (
  @ForgetPasswordRequestID int
)
AS
  DELETE FROM [dbo].[ForgetPasswordRequest]
  WHERE 
    ([ForgetPasswordRequestID] = @ForgetPasswordRequestID)