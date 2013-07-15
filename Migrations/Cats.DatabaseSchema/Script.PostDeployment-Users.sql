/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*
	This will create the default 'admin' account with password 'password'
*/
--INSERT INTO [User](UserName,FullName,Email,[Password],[Disabled]) 
--VALUES('admin','Administrator','admin@catsproject.org','4gEGXQVUZSYVwyDACh1byO3KRp1ywnkOJBUtDB4rYYk=',False)