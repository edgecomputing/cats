CREATE TABLE [dbo].[ApplicationSetting] (
    [SettingID]    INT          IDENTITY (1, 1) NOT NULL,
    [SettingName]  VARCHAR (50) NOT NULL,
    [SettingValue] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ApplicationSetting] PRIMARY KEY CLUSTERED ([SettingID] ASC)
);



