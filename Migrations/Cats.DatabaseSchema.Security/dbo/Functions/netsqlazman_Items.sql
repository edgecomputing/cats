CREATE FUNCTION [dbo].[netsqlazman_Items] ()
RETURNS TABLE
AS
RETURN
	SELECT     dbo.[netsqlazman_ItemsTable].*
	FROM         dbo.[netsqlazman_ItemsTable] INNER JOIN
	                      dbo.[netsqlazman_Applications]() Applications ON dbo.[netsqlazman_ItemsTable].ApplicationId = Applications.ApplicationId

GO
GRANT SELECT
    ON OBJECT::[dbo].[netsqlazman_Items] TO [NetSqlAzMan_Readers]
    AS [dbo];

