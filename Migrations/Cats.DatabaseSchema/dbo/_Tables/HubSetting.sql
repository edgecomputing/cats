CREATE TABLE [dbo].[HubSetting] (
    [HubSettingID] INT            IDENTITY (1, 1) NOT NULL,
    [Key]          NVARCHAR (50)  NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [ValueType]    NVARCHAR (50)  NULL,
    [Options]      NVARCHAR (500) NULL,
    CONSTRAINT [PK_WarehouseSetting] PRIMARY KEY CLUSTERED ([HubSettingID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'This table represents setting entries that are specific to the hubs.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubSetting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key of the hub settings table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubSetting', @level2type = N'COLUMN', @level2name = N'HubSettingID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The string value of the ''key''. this should be an equivalent of the constant', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubSetting', @level2type = N'COLUMN', @level2name = N'Key';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The dispalyed name of the settings,', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubSetting', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The value type of the settings, this is a field that tracks the value type such as int, string, email, etc....', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubSetting', @level2type = N'COLUMN', @level2name = N'ValueType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'If the value is to be selected from a list, this column contains the options that have to be displayed.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HubSetting', @level2type = N'COLUMN', @level2name = N'Options';

