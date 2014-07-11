

CREATE PROCEDURE [sp_ins_UserProfile] (
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
  INSERT INTO [dbo].[UserProfile] (
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
  )
  VALUES (
    @UserName,
    @Password,
    @FirstName,
    @LastName,
    @GrandFatherName,
    @ActiveInd,
    @LoggedInInd,
    @LogginDate,
    @LogOutDate,
    @FailedAttempts,
    @LockedInInd,
    @LanguageCode,
    @MobileNumber,
    @Email,
    @DefaultTheme
  )