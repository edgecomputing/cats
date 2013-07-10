

CREATE PROCEDURE [sp_ins_SessionHistory] (
  @UserProfileID int,
  @RoleID int,
  @LoginDate datetime,
  @UserName varchar(50),
  @Password varchar(50),
  @WorkstationName varchar(50),
  @IPAddress varchar(50),
  @ApplicationName varchar(50)
)
AS
  INSERT INTO [dbo].[SessionHistory] (
    [UserProfileID],
    [RoleID],
    [LoginDate],
    [UserName],
    [Password],
    [WorkstationName],
    [IPAddress],
    [ApplicationName]
  )
  VALUES (
    @UserProfileID,
    @RoleID,
    @LoginDate,
    @UserName,
    @Password,
    @WorkstationName,
    @IPAddress,
    @ApplicationName
  )