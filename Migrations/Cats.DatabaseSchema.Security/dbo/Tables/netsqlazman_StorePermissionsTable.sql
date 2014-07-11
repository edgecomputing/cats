CREATE TABLE [dbo].[netsqlazman_StorePermissionsTable] (
    [StorePermissionId]          INT            IDENTITY (1, 1) NOT NULL,
    [StoreId]                    INT            NOT NULL,
    [SqlUserOrRole]              NVARCHAR (128) NOT NULL,
    [IsSqlRole]                  BIT            NOT NULL,
    [NetSqlAzManFixedServerRole] TINYINT        NOT NULL,
    CONSTRAINT [PK_StorePermissions] PRIMARY KEY CLUSTERED ([StorePermissionId] ASC),
    CONSTRAINT [CK_StorePermissions] CHECK ([NetSqlAzManFixedServerRole]>=(0) AND [NetSqlAzManFixedServerRole]<=(2)),
    CONSTRAINT [FK_StorePermissions_StoresTable] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[netsqlazman_StoresTable] ([StoreId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_StorePermissions]
    ON [dbo].[netsqlazman_StorePermissionsTable]([StoreId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StorePermissions_1]
    ON [dbo].[netsqlazman_StorePermissionsTable]([StoreId] ASC, [SqlUserOrRole] ASC, [NetSqlAzManFixedServerRole] ASC);


GO
GRANT SELECT
    ON OBJECT::[dbo].[netsqlazman_StorePermissionsTable] TO [NetSqlAzMan_Readers]
    AS [dbo];

