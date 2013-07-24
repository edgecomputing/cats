CREATE ROLE [NetSqlAzMan_Managers]
    AUTHORIZATION [dbo];


GO
ALTER ROLE [NetSqlAzMan_Managers] ADD MEMBER [NetSqlAzMan_Administrators];


GO
ALTER ROLE [NetSqlAzMan_Managers] ADD MEMBER [BUILTIN\Administrators];

