
CREATE PROCEDURE [sp_sel_SessionHistory]
AS
  SELECT 
    [SessionHistoryID],
    [UserProfileID],
    [RoleID],
    [LoginDate],
    [UserName],
    [Password],
    [WorkstationName],
    [IPAddress],
    [ApplicationName]
  FROM 
    [dbo].[SessionHistory]