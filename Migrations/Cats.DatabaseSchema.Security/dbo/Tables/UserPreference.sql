CREATE TABLE [dbo].[UserPreference] (
    [UserAccountId]            INT           NOT NULL,
    [LanguageCode]             CHAR (2)      CONSTRAINT [DF_UserPreference_LanguageCode] DEFAULT ('en') NOT NULL,
    [Calendar]                 CHAR (2)      CONSTRAINT [DF_UserPreference_Calendar] DEFAULT ('en') NOT NULL,
    [Keyboard]                 CHAR (2)      NOT NULL,
    [PreferedWeightMeasurment] CHAR (2)      CONSTRAINT [DF_UserPreference_PreferedWeightMeasurment] DEFAULT ('MT') NOT NULL,
    [DefaultTheme]             NVARCHAR (50) CONSTRAINT [DF_UserPreference_DefaultTheme] DEFAULT (N'metro') NOT NULL,
    CONSTRAINT [PK_UserPreference] PRIMARY KEY CLUSTERED ([UserAccountId] ASC),
    CONSTRAINT [FK_UserPreference_UserAccount] FOREIGN KEY ([UserAccountId]) REFERENCES [dbo].[UserAccount] ([UserAccountId])
);

