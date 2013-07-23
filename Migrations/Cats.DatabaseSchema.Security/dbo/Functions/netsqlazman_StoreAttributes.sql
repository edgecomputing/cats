CREATE FUNCTION [dbo].[netsqlazman_StoreAttributes] ()
RETURNS TABLE
AS
RETURN
	SELECT     dbo.[netsqlazman_StoreAttributesTable].*
	FROM         dbo.[netsqlazman_StoreAttributesTable] INNER JOIN
	                      dbo.[netsqlazman_Stores]() Stores ON dbo.[netsqlazman_StoreAttributesTable].StoreId = Stores.StoreId

GO
GRANT SELECT
    ON OBJECT::[dbo].[netsqlazman_StoreAttributes] TO [NetSqlAzMan_Readers]
    AS [dbo];

