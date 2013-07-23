CREATE PROCEDURE [dbo].[netsqlazman_StoreGroupUpdate]
(
	@StoreId int,
	@objectSid varbinary(85),
	@Name nvarchar(255),
	@Description nvarchar(1024),
	@LDapQuery nvarchar(4000),
	@GroupType tinyint,
	@Original_StoreGroupId int
)
AS
IF dbo.[netsqlazman_CheckStorePermissions](@StoreId, 2) = 1
	UPDATE [dbo].[netsqlazman_StoreGroupsTable] SET [objectSid] = @objectSid, [Name] = @Name, [Description] = @Description, [LDapQuery] = @LDapQuery, [GroupType] = @GroupType WHERE [StoreGroupId] = @Original_StoreGroupId AND StoreId = @StoreId
ELSE
	RAISERROR ('Store permission denied.', 16, 1)

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[netsqlazman_StoreGroupUpdate] TO [NetSqlAzMan_Managers]
    AS [dbo];

