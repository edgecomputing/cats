CREATE TABLE [dbo].[SessionAttempts] (
    [SessionAttemptID] UNIQUEIDENTIFIER CONSTRAINT [DF_SessionAttempts_SessionAttemptsID] DEFAULT (newsequentialid()) NOT NULL,
    [UserProfileID]    INT              NOT NULL,
    [PartitionID]      INT              CONSTRAINT [DF_SessionAttempts_PartitionID] DEFAULT ((0)) NOT NULL,
    [RoleID]           INT              NULL,
    [LoginDate]        DATETIME         NOT NULL,
    [UserName]         VARCHAR (50)     NULL,
    [Password]         VARCHAR (50)     NULL,
    [WorkstationName]  VARCHAR (50)     NULL,
    [IPAddress]        VARCHAR (50)     NULL,
    [ApplicationName]  VARCHAR (50)     NULL,
    CONSTRAINT [PK_SessionAttempts] PRIMARY KEY CLUSTERED ([SessionAttemptID] ASC),
    CONSTRAINT [FK_SessionAttempts_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([RoleID]),
    CONSTRAINT [FK_SessionAttempts_UserProfile] FOREIGN KEY ([UserProfileID]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);

