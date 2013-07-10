
CREATE PROCEDURE [sp_sel_ForgetPasswordRequest]
AS
  SELECT 
    [ForgetPasswordRequestID],
    [UserProfileID],
    [GeneratedDate],
    [ExpieryDate],
    [Completed],
    [RequestKey]
  FROM 
    [dbo].[ForgetPasswordRequest]