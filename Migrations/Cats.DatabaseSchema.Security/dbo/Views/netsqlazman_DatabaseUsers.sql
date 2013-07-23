CREATE VIEW [dbo].[netsqlazman_DatabaseUsers]
AS
SELECT     *
FROM         dbo.[netsqlazman_GetDBUsers](NULL, NULL, DEFAULT, DEFAULT) GetDBUsers

GO
GRANT SELECT
    ON OBJECT::[dbo].[netsqlazman_DatabaseUsers] TO [NetSqlAzMan_Readers]
    AS [dbo];

