CREATE FUNCTION [dbo].[netsqlazman_StoreGroups] ()
RETURNS TABLE
AS
RETURN
	SELECT     dbo.[netsqlazman_StoreGroupsTable].*
	FROM         dbo.[netsqlazman_Stores]() Stores INNER JOIN
	                      dbo.[netsqlazman_StoreGroupsTable] ON Stores.StoreId = dbo.[netsqlazman_StoreGroupsTable].StoreId

GO
GRANT SELECT
    ON OBJECT::[dbo].[netsqlazman_StoreGroups] TO [NetSqlAzMan_Readers]
    AS [dbo];

