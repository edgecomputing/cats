CREATE TABLE [dbo].[netsqlazman_ApplicationPermissionsTable] (
    [ApplicationPermissionId]    INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationId]              INT            NOT NULL,
    [SqlUserOrRole]              NVARCHAR (128) NOT NULL,
    [IsSqlRole]                  BIT            NOT NULL,
    [NetSqlAzManFixedServerRole] TINYINT        NOT NULL,
    CONSTRAINT [PK_ApplicationPermissions] PRIMARY KEY CLUSTERED ([ApplicationPermissionId] ASC),
    CONSTRAINT [CK_ApplicationPermissions] CHECK ([NetSqlAzManFixedServerRole]>=(0) AND [NetSqlAzManFixedServerRole]<=(2)),
    CONSTRAINT [FK_ApplicationPermissions_ApplicationsTable] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_ApplicationPermissions]
    ON [dbo].[netsqlazman_ApplicationPermissionsTable]([ApplicationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationPermissions_1]
    ON [dbo].[netsqlazman_ApplicationPermissionsTable]([ApplicationId] ASC, [SqlUserOrRole] ASC, [NetSqlAzManFixedServerRole] ASC);


GO
GRANT SELECT
    ON OBJECT::[dbo].[netsqlazman_ApplicationPermissionsTable] TO [NetSqlAzMan_Readers]
    AS [dbo];

