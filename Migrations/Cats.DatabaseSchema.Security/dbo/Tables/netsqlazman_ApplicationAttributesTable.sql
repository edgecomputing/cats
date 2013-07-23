CREATE TABLE [dbo].[netsqlazman_ApplicationAttributesTable] (
    [ApplicationAttributeId] INT             IDENTITY (1, 1) NOT NULL,
    [ApplicationId]          INT             NOT NULL,
    [AttributeKey]           NVARCHAR (255)  NOT NULL,
    [AttributeValue]         NVARCHAR (4000) NOT NULL,
    CONSTRAINT [PK_ApplicationAttributes] PRIMARY KEY CLUSTERED ([ApplicationAttributeId] ASC),
    CONSTRAINT [FK_ApplicationAttributes_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ApplicationAttributes_AuhorizationId_AttributeKey_Unique_Index]
    ON [dbo].[netsqlazman_ApplicationAttributesTable]([ApplicationId] ASC, [AttributeKey] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationAttributes]
    ON [dbo].[netsqlazman_ApplicationAttributesTable]([ApplicationId] ASC, [AttributeKey] ASC);

