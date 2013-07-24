CREATE TABLE [dbo].[netsqlazman_AuthorizationsTable] (
    [AuthorizationId]       INT            IDENTITY (1, 1) NOT NULL,
    [ItemId]                INT            NOT NULL,
    [ownerSid]              VARBINARY (85) NOT NULL,
    [ownerSidWhereDefined]  TINYINT        NOT NULL,
    [objectSid]             VARBINARY (85) NOT NULL,
    [objectSidWhereDefined] TINYINT        NOT NULL,
    [AuthorizationType]     TINYINT        NOT NULL,
    [ValidFrom]             DATETIME       NULL,
    [ValidTo]               DATETIME       NULL,
    CONSTRAINT [PK_Authorizations] PRIMARY KEY CLUSTERED ([AuthorizationId] ASC),
    CONSTRAINT [CK_AuthorizationTypeCheck] CHECK ([AuthorizationType]>=(0) AND [AuthorizationType]<=(3)),
    CONSTRAINT [CK_objectSidWhereDefinedCheck] CHECK ([objectSidWhereDefined]>=(0) AND [objectSidWhereDefined]<=(4)),
    CONSTRAINT [CK_ownerSidWhereDefined] CHECK ([ownerSidWhereDefined]>=(2) AND [ownerSidWhereDefined]<=(4)),
    CONSTRAINT [CK_ValidFromToCheck] CHECK ([ValidFrom] IS NULL OR [ValidTo] IS NULL OR [ValidFrom]<=[ValidTo]),
    CONSTRAINT [FK_Authorizations_Items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[netsqlazman_ItemsTable] ([ItemId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Authorizations]
    ON [dbo].[netsqlazman_AuthorizationsTable]([ItemId] ASC, [objectSid] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Authorizations_1]
    ON [dbo].[netsqlazman_AuthorizationsTable]([ItemId] ASC, [objectSid] ASC, [objectSidWhereDefined] ASC, [AuthorizationType] ASC, [ValidFrom] ASC, [ValidTo] ASC);

