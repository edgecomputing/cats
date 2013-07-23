CREATE FUNCTION [dbo].[netsqlazman_ApplicationGroupMembers] ()
RETURNS TABLE
AS
RETURN
	SELECT     dbo.[netsqlazman_ApplicationGroupMembersTable].*
	FROM         dbo.[netsqlazman_ApplicationGroups]() ApplicationGroups INNER JOIN
	                      dbo.[netsqlazman_ApplicationGroupMembersTable] ON ApplicationGroups.ApplicationGroupId = dbo.[netsqlazman_ApplicationGroupMembersTable].ApplicationGroupId

GO
GRANT SELECT
    ON OBJECT::[dbo].[netsqlazman_ApplicationGroupMembers] TO [NetSqlAzMan_Readers]
    AS [dbo];

