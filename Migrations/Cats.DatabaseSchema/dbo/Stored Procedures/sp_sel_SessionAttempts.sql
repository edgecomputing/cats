
CREATE PROCEDURE [sp_sel_SessionAttempts]
AS
  SELECT 
    [SessionAttemptID],
    [UserProfileID],
    [RoleID],
    [LoginDate],
    [UserName],
    [Password],
    [WorkstationName],
    [IPAddress],
    [ApplicationName]
  FROM 
    [dbo].[SessionAttempts]