CREATE TABLE [dbo].[netsqlazman_StoresTable] (
    [StoreId]     INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255)  NOT NULL,
    [Description] NVARCHAR (1024) NOT NULL,
    CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED ([StoreId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Stores_Name_Unique_Index]
    ON [dbo].[netsqlazman_StoresTable]([Name] ASC);

