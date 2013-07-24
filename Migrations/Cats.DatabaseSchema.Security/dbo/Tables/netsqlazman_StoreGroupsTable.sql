CREATE TABLE [dbo].[netsqlazman_StoreGroupsTable] (
    [StoreGroupId] INT             IDENTITY (1, 1) NOT NULL,
    [StoreId]      INT             NOT NULL,
    [objectSid]    VARBINARY (85)  NOT NULL,
    [Name]         NVARCHAR (255)  NOT NULL,
    [Description]  NVARCHAR (1024) NOT NULL,
    [LDapQuery]    NVARCHAR (4000) NULL,
    [GroupType]    TINYINT         NOT NULL,
    CONSTRAINT [PK_StoreGroups] PRIMARY KEY CLUSTERED ([StoreGroupId] ASC),
    CONSTRAINT [CK_StoreGroups_GroupType_Check] CHECK ([GroupType]>=(0) AND [GroupType]<=(1)),
    CONSTRAINT [FK_StoreGroups_Stores] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[netsqlazman_StoresTable] ([StoreId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_StoreGroups]
    ON [dbo].[netsqlazman_StoreGroupsTable]([StoreId] ASC, [objectSid] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [StoreGroups_StoreId_Name_Unique_Index]
    ON [dbo].[netsqlazman_StoreGroupsTable]([StoreId] ASC, [Name] ASC);


GO
CREATE TRIGGER [dbo].[StoreGroupDeleteTrigger] ON [dbo].[netsqlazman_StoreGroupsTable] 
FOR DELETE 
AS
DECLARE @DELETEDOBJECTSID int
DECLARE storegroups_cur CURSOR FAST_FORWARD FOR SELECT objectSid FROM deleted
OPEN storegroups_cur
FETCH NEXT from storegroups_cur INTO @DELETEDOBJECTSID
WHILE @@FETCH_STATUS = 0
BEGIN
	DELETE FROM dbo.[netsqlazman_StoreGroupMembersTable] WHERE objectSid = @DELETEDOBJECTSID AND WhereDefined = 0
	DELETE FROM dbo.[netsqlazman_ApplicationGroupMembersTable] WHERE objectSid = @DELETEDOBJECTSID AND WhereDefined = 0
	DELETE FROM dbo.[netsqlazman_AuthorizationsTable] WHERE objectSid = @DELETEDOBJECTSID AND objectSidWhereDefined = 0
	FETCH NEXT from storegroups_cur INTO @DELETEDOBJECTSID
END
CLOSE storegroups_cur
DEALLOCATE storegroups_cur
