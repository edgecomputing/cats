CREATE FUNCTION [dbo].[netsqlazman_DBVersion] ()  
RETURNS nvarchar(200) AS  
BEGIN 
	return '3.6.0.x'
END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[netsqlazman_DBVersion] TO [NetSqlAzMan_Readers]
    AS [dbo];

