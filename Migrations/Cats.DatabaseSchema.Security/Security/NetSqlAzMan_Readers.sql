CREATE ROLE [NetSqlAzMan_Readers]
    AUTHORIZATION [dbo];


GO
ALTER ROLE [NetSqlAzMan_Readers] ADD MEMBER [NetSqlAzMan_Users];


GO
ALTER ROLE [NetSqlAzMan_Readers] ADD MEMBER [NetSqlAzMan_Managers];


GO
ALTER ROLE [NetSqlAzMan_Readers] ADD MEMBER [NetSqlAzMan_Administrators];


GO
ALTER ROLE [NetSqlAzMan_Readers] ADD MEMBER [BUILTIN\Administrators];

