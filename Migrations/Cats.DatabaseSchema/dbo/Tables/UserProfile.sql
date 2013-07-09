CREATE TABLE [dbo].[UserProfile] (
    [UserProfileID]            INT           IDENTITY (1, 1) NOT NULL,
    [UserName]                 VARCHAR (50)  NOT NULL,
    [Password]                 VARCHAR (50)  NOT NULL,
    [FirstName]                VARCHAR (30)  NOT NULL,
    [LastName]                 VARCHAR (30)  NOT NULL,
    [GrandFatherName]          VARCHAR (30)  NULL,
    [ActiveInd]                BIT           NOT NULL,
    [LoggedInInd]              BIT           NOT NULL,
    [LogginDate]               DATETIME      NULL,
    [LogOutDate]               DATETIME      NULL,
    [FailedAttempts]           INT           NOT NULL,
    [LockedInInd]              BIT           NOT NULL,
    [LanguageCode]             CHAR (2)      CONSTRAINT [DF_UserProfile_LanguageCode] DEFAULT ('en') NOT NULL,
    [DatePreference]           CHAR (2)      CONSTRAINT [DF_UserProfile_PreferredDateFormat] DEFAULT ('en') NOT NULL,
    [PreferedWeightMeasurment] CHAR (2)      CONSTRAINT [DF_UserProfile_PreferedWeightMeasurment] DEFAULT ('MT') NOT NULL,
    [MobileNumber]             VARCHAR (20)  NULL,
    [Email]                    VARCHAR (100) NULL,
    [DefaultTheme]             NVARCHAR (50) CONSTRAINT [DF_UserProfile_DefaultTheme] DEFAULT (N'metro') NOT NULL,
    CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED ([UserProfileID] ASC)
);

