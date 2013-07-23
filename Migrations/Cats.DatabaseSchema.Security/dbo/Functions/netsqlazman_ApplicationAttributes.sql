CREATE FUNCTION [dbo].[netsqlazman_ApplicationAttributes] ()
RETURNS TABLE
AS
RETURN 
	SELECT     dbo.[netsqlazman_ApplicationAttributesTable].*
	FROM         dbo.[netsqlazman_ApplicationAttributesTable] INNER JOIN
	                      dbo.[netsqlazman_Applications]() Applications ON dbo.[netsqlazman_ApplicationAttributesTable].ApplicationId = Applications.ApplicationId

GO
GRANT SELECT
    ON OBJECT::[dbo].[netsqlazman_ApplicationAttributes] TO [NetSqlAzMan_Readers]
    AS [dbo];

