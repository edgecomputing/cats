CREATE TABLE [dbo].[netsqlazman_AuthorizationAttributesTable] (
    [AuthorizationAttributeId] INT             IDENTITY (1, 1) NOT NULL,
    [AuthorizationId]          INT             NOT NULL,
    [AttributeKey]             NVARCHAR (255)  NOT NULL,
    [AttributeValue]           NVARCHAR (4000) NOT NULL,
    CONSTRAINT [PK_AuthorizationAttributes] PRIMARY KEY CLUSTERED ([AuthorizationAttributeId] ASC),
    CONSTRAINT [FK_AuthorizationAttributes_Authorizations] FOREIGN KEY ([AuthorizationId]) REFERENCES [dbo].[netsqlazman_AuthorizationsTable] ([AuthorizationId]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [AuthorizationAttributes_AuhorizationId_AttributeKey_Unique_Index]
    ON [dbo].[netsqlazman_AuthorizationAttributesTable]([AuthorizationId] ASC, [AttributeKey] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AuthorizationAttributes]
    ON [dbo].[netsqlazman_AuthorizationAttributesTable]([AuthorizationId] ASC, [AttributeKey] ASC);

