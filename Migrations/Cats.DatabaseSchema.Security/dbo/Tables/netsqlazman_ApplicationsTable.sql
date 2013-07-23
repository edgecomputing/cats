CREATE TABLE [dbo].[netsqlazman_ApplicationsTable] (
    [ApplicationId] INT             IDENTITY (1, 1) NOT NULL,
    [StoreId]       INT             NOT NULL,
    [Name]          NVARCHAR (255)  NOT NULL,
    [Description]   NVARCHAR (1024) NOT NULL,
    CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ([ApplicationId] ASC),
    CONSTRAINT [FK_Applications_Stores] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[netsqlazman_StoresTable] ([StoreId]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Applications_StoreId_Name_Unique_Index]
    ON [dbo].[netsqlazman_ApplicationsTable]([Name] ASC, [StoreId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Applications]
    ON [dbo].[netsqlazman_ApplicationsTable]([ApplicationId] ASC, [Name] ASC);

