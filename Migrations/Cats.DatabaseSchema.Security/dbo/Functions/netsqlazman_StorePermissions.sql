CREATE FUNCTION [dbo].[netsqlazman_StorePermissions]()
RETURNS TABLE 
AS  
RETURN
	SELECT     dbo.[netsqlazman_StorePermissionsTable].*
	FROM         dbo.[netsqlazman_StorePermissionsTable] INNER JOIN
	                      dbo.[netsqlazman_Stores]() Stores ON dbo.[netsqlazman_StorePermissionsTable].StoreId = Stores.StoreId

GO
GRANT SELECT
    ON OBJECT::[dbo].[netsqlazman_StorePermissions] TO [NetSqlAzMan_Readers]
    AS [dbo];

