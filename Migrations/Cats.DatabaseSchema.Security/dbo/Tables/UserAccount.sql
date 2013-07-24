CREATE TABLE [dbo].[UserAccount] (
    [UserAccountId]  INT            IDENTITY (1, 1) NOT NULL,
    [UserName]       NVARCHAR (200) NOT NULL,
    [Password]       NVARCHAR (MAX) NOT NULL,
    [Disabled]       BIT            CONSTRAINT [DF_UserAccount_Disabled] DEFAULT ((0)) NOT NULL,
    [LoggedIn]       BIT            CONSTRAINT [DF_UserAccount_LoggedIn] DEFAULT ((0)) NOT NULL,
    [LogginDate]     DATETIME       NULL,
    [LogOutDate]     DATETIME       NULL,
    [FailedAttempts] INT            CONSTRAINT [DF_UserAccount_FailedAttempts] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED ([UserAccountId] ASC),
    CONSTRAINT [IX_UserAccount_UserName] UNIQUE NONCLUSTERED ([UserName] ASC)
);

