CREATE TABLE [dbo].[Hub] (
    [HubID]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [HubOwnerID] INT           NOT NULL,
    CONSTRAINT [PK_Warehouse] PRIMARY KEY CLUSTERED ([HubID] ASC),
    CONSTRAINT [FK_Warehouse_WarehouseOwner] FOREIGN KEY ([HubOwnerID]) REFERENCES [dbo].[HubOwner] ([HubOwnerID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hubs owned by all hub owners are listed here.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Hub';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key of the hubs', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Hub', @level2type = N'COLUMN', @level2name = N'HubID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the hub.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Hub', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Owner of the hub', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Hub', @level2type = N'COLUMN', @level2name = N'HubOwnerID';

