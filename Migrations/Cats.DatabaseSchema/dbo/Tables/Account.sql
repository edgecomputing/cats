CREATE TABLE [dbo].[Account] (
    [AccountID]  INT           IDENTITY (1, 1) NOT NULL,
    [EntityType] NVARCHAR (50) NOT NULL,
    [EntityID]   INT           NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([AccountID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Accounts table is a proxy table that uniquely identifies  different types of entities. Some of the entities that have keys in the Account table are Donors, Warehouse Owners, FDPs, and Hubs. This table is mainly used in the Transaction sub system.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Account';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary Key Field', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Account', @level2type = N'COLUMN', @level2name = N'AccountID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Holds the table name of the entity that is being referenced in the database', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Account', @level2type = N'COLUMN', @level2name = N'EntityType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The ID of the Entity Being referenced. This is a value from from the different entity tables.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Account', @level2type = N'COLUMN', @level2name = N'EntityID';

