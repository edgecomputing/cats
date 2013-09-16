CREATE TABLE [dbo].[HubOwner] (
    [HubOwnerID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50)  NOT NULL,
    [LongName]   NVARCHAR (500) NULL,
    CONSTRAINT [PK_WarehouseOwner] PRIMARY KEY CLUSTERED ([HubOwnerID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tracks the different Hub owners.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubOwner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The hub owner ID, a primary key field for the hub owner table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubOwner', @level2type = N'COLUMN', @level2name = N'HubOwnerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the Hub Owner', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubOwner', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'When the name of the hub owner is an abbreviation, this field contains the long version of the name.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubOwner', @level2type = N'COLUMN', @level2name = N'LongName';

