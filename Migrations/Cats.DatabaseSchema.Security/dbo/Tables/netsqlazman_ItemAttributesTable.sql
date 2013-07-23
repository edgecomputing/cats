CREATE TABLE [dbo].[netsqlazman_ItemAttributesTable] (
    [ItemAttributeId] INT             IDENTITY (1, 1) NOT NULL,
    [ItemId]          INT             NOT NULL,
    [AttributeKey]    NVARCHAR (255)  NOT NULL,
    [AttributeValue]  NVARCHAR (4000) NOT NULL,
    CONSTRAINT [PK_ItemAttributes] PRIMARY KEY CLUSTERED ([ItemAttributeId] ASC),
    CONSTRAINT [FK_ItemAttributes_Items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[netsqlazman_ItemsTable] ([ItemId]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ItemAttributes_AuhorizationId_AttributeKey_Unique_Index]
    ON [dbo].[netsqlazman_ItemAttributesTable]([ItemId] ASC, [AttributeKey] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ItemAttributes]
    ON [dbo].[netsqlazman_ItemAttributesTable]([ItemId] ASC, [AttributeKey] ASC);

