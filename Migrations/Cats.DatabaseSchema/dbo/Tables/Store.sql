CREATE TABLE [dbo].[Store] (
    [StoreID]      INT           IDENTITY (1, 1) NOT NULL,
    [Number]       INT           CONSTRAINT [DF_Store_Number] DEFAULT ((0)) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [HubID]        INT           NOT NULL,
    [IsTemporary]  BIT           CONSTRAINT [DF_Store_IsTemporary] DEFAULT ((0)) NOT NULL,
    [IsActive]     BIT           CONSTRAINT [DF_Store_IsActive] DEFAULT ((1)) NOT NULL,
    [StackCount]   INT           NOT NULL,
    [StoreManName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED ([StoreID] ASC),
    CONSTRAINT [FK_Store_Warehouse] FOREIGN KEY ([HubID]) REFERENCES [dbo].[Hub] ([HubID])
);

