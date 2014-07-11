

CREATE PROCEDURE [sp_upd_UserProfile] (
  @UserProfileID int,
  @UserName varchar(50),
  @Password varchar(50),
  @FirstName varchar(30),
  @LastName varchar(30),
  @GrandFatherName varchar(30),
  @ActiveInd bit,
  @LoggedInInd bit,
  @LogginDate datetime,
  @LogOutDate datetime,
  @FailedAttempts int,
  @LockedInInd bit,
  @LanguageCode char(2),
  @MobileNumber varchar(20),
  @Email varchar(100),
  @DefaultTheme nvarchar(50)
)
AS
  UPDATE [dbo].[UserProfile] SET
    [UserName] = @UserName,
    [Password] = @Password,
    [FirstName] = @FirstName,
    [LastName] = @LastName,
    [GrandFatherName] = @GrandFatherName,
    [ActiveInd] = @ActiveInd,
    [LoggedInInd] = @LoggedInInd,
    [LogginDate] = @LogginDate,
    [LogOutDate] = @LogOutDate,
    [FailedAttempts] = @FailedAttempts,
    [LockedInInd] = @LockedInInd,
    [LanguageCode] = @LanguageCode,
    [MobileNumber] = @MobileNumber,
    [Email] = @Email,
    [DefaultTheme] = @DefaultTheme
  WHERE 
    ([UserProfileID] = @UserProfileID)