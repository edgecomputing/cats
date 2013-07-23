CREATE FUNCTION [dbo].[netsqlazman_AuthorizationAttributes] ()
RETURNS TABLE
AS
RETURN
	SELECT     dbo.[netsqlazman_AuthorizationAttributesTable].*
	FROM         dbo.[netsqlazman_AuthorizationAttributesTable] INNER JOIN
	                      dbo.[netsqlazman_Authorizations]() as Authorizations ON dbo.[netsqlazman_AuthorizationAttributesTable].AuthorizationId = Authorizations.AuthorizationId

GO
GRANT SELECT
    ON OBJECT::[dbo].[netsqlazman_AuthorizationAttributes] TO [NetSqlAzMan_Readers]
    AS [dbo];

