CREATE TABLE [dbo].[netsqlazman_StoreAttributesTable] (
    [StoreAttributeId] INT             IDENTITY (1, 1) NOT NULL,
    [StoreId]          INT             NOT NULL,
    [AttributeKey]     NVARCHAR (255)  NOT NULL,
    [AttributeValue]   NVARCHAR (4000) NOT NULL,
    CONSTRAINT [PK_StoreAttributes] PRIMARY KEY CLUSTERED ([StoreAttributeId] ASC),
    CONSTRAINT [FK_StoreAttributes_Stores] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[netsqlazman_StoresTable] ([StoreId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_StoreAttributes]
    ON [dbo].[netsqlazman_StoreAttributesTable]([StoreId] ASC, [AttributeKey] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [StoreAttributes_AuhorizationId_AttributeKey_Unique_Index]
    ON [dbo].[netsqlazman_StoreAttributesTable]([StoreId] ASC, [AttributeKey] ASC);

