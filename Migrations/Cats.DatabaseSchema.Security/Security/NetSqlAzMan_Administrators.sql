CREATE ROLE [NetSqlAzMan_Administrators]
    AUTHORIZATION [dbo];


GO
ALTER ROLE [NetSqlAzMan_Administrators] ADD MEMBER [BUILTIN\Administrators];

