
CREATE PROCEDURE [sp_sel_UserProfile]
AS
  SELECT 
    [UserProfileID],
    [UserName],
    [Password],
    [FirstName],
    [LastName],
    [GrandFatherName],
    [ActiveInd],
    [LoggedInInd],
    [LogginDate],
    [LogOutDate],
    [FailedAttempts],
    [LockedInInd],
    [LanguageCode],
    [MobileNumber],
    [Email],
    [DefaultTheme]
  FROM 
    [dbo].[UserProfile]