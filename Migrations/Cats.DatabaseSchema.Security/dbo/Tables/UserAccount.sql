CREATE TABLE [dbo].[UserAccount] (
    [UserAccountId]            INT            IDENTITY (1, 1) NOT NULL,
    [UserName]                 NVARCHAR (200) NOT NULL,
    [Password]                 NVARCHAR (MAX) NOT NULL,
    [Disabled]                 BIT            CONSTRAINT [DF_UserAccount_Disabled] DEFAULT ((0)) NOT NULL,
    [LoggedIn]                 BIT            CONSTRAINT [DF_UserAccount_LoggedIn] DEFAULT ((0)) NOT NULL,
    [LogginDate]               DATETIME       NULL,
    [LogOutDate]               DATETIME       NULL,
    [FailedAttempts]           INT            CONSTRAINT [DF_UserAccount_FailedAttempts] DEFAULT ((0)) NOT NULL,
    [FirstName]                NVARCHAR (200) NOT NULL,
    [LastName]                 NVARCHAR (200) NOT NULL,
    [GrandFatherName]          NVARCHAR (200) NULL,
    [Email]                    NVARCHAR (50)  NULL,
    [CaseTeam]                 INT            NULL,
    [LanguageCode]             CHAR (2)       CONSTRAINT [DF_UserAccount_LanguageCode] DEFAULT ('en') NOT NULL,
    [Calendar]                 CHAR (2)       CONSTRAINT [DF_UserAccount_Calendar] DEFAULT ('en') NOT NULL,
    [Keyboard]                 CHAR (2)       NOT NULL,
    [PreferedWeightMeasurment] CHAR (2)       CONSTRAINT [DF_UserAccount_PreferedWeightMeasurment] DEFAULT ('MT') NOT NULL,
    [DefaultTheme]             NVARCHAR (50)  CONSTRAINT [DF_UserAccount_DefaultTheme] DEFAULT (N'metro') NOT NULL,
    CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED ([UserAccountId] ASC),
    CONSTRAINT [IX_UserAccount_UserName] UNIQUE NONCLUSTERED ([UserName] ASC)
);



