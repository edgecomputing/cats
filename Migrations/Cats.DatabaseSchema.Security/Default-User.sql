/*
	This will create the default 'admin' account with password 'password'
	WARNING: This script will delete all user accounts
*/

Delete UserProfile 
go

Delete UserPreference
go
DELETE UserAccount

GO

 DBCC CHECKIDENT('UserAccount', RESEED,0)


GO


INSERT INTO [UserAccount] (UserName,[Password],[Disabled],LoggedIn,FailedAttempts)
					VALUES('admin','4gEGXQVUZSYVwyDACh1byO3KRp1ywnkOJBUtDB4rYYk=',0,0,0)

GO

INSERT INTO [UserProfile] (UserAccountId,FirstName,LastName,GrandFatherName,Email)
					VALUES(1,'Administrator','S','A','admin@catsproject.org')

GO

INSERT INTO [UserPreference](UserAccountId,LanguageCode,Calendar,Keyboard,PreferedWeightMeasurment,DefaultTheme)
					VALUES(1,'EN','ET','AM','QT','Default')
