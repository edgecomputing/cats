CREATE ROLE [NetSqlAzMan_Users]
    AUTHORIZATION [dbo];


GO
ALTER ROLE [NetSqlAzMan_Users] ADD MEMBER [NetSqlAzMan_Managers];


GO
ALTER ROLE [NetSqlAzMan_Users] ADD MEMBER [NetSqlAzMan_Administrators];


GO
ALTER ROLE [NetSqlAzMan_Users] ADD MEMBER [BUILTIN\Administrators];

