CREATE TABLE [dbo].[netsqlazman_Settings] (
    [SettingName]  NVARCHAR (255) NOT NULL,
    [SettingValue] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([SettingName] ASC),
    CONSTRAINT [CK_Settings] CHECK ([SettingName]='Mode' AND ([SettingValue]='Developer' OR [SettingValue]='Administrator') OR [SettingName]='LogErrors' AND ([SettingValue]='True' OR [SettingValue]='False') OR [SettingName]='LogWarnings' AND ([SettingValue]='True' OR [SettingValue]='False') OR [SettingName]='LogInformations' AND ([SettingValue]='True' OR [SettingValue]='False') OR [SettingName]='LogOnEventLog' AND ([SettingValue]='True' OR [SettingValue]='False') OR [SettingName]='LogOnDb' AND ([SettingValue]='True' OR [SettingValue]='False'))
);

