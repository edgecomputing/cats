CREATE TABLE [dbo].[HubSettingValue] (
    [HubSettingValueID] INT             IDENTITY (1, 1) NOT NULL,
    [HubSettingID]      INT             NOT NULL,
    [HubID]             INT             NOT NULL,
    [Value]             NVARCHAR (4000) NULL,
    CONSTRAINT [PK_WarehouseSettingValue] PRIMARY KEY CLUSTERED ([HubSettingValueID] ASC),
    CONSTRAINT [FK_WarehouseSettingValue_Warehouse] FOREIGN KEY ([HubID]) REFERENCES [dbo].[Hub] ([HubID]),
    CONSTRAINT [FK_WarehouseSettingValue_WarehouseSetting] FOREIGN KEY ([HubSettingID]) REFERENCES [dbo].[HubSetting] ([HubSettingID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contains the specific values of the Hub setting entries.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubSettingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key of the hub settings value.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubSettingValue', @level2type = N'COLUMN', @level2name = N'HubSettingValueID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The settings ID, for which the value is about to be shown', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubSettingValue', @level2type = N'COLUMN', @level2name = N'HubSettingID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A reference to the hub that has the values specified.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubSettingValue', @level2type = N'COLUMN', @level2name = N'HubID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The value of the option. (A serialized, text value of the option value)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubSettingValue', @level2type = N'COLUMN', @level2name = N'Value';

