CREATE TABLE [dbo].[SessionHistory] (
    [SessionHistoryID] UNIQUEIDENTIFIER CONSTRAINT [DF_SessionHistory_SessionHistoryID] DEFAULT (newsequentialid()) NOT NULL,
    [PartitionID]      INT              CONSTRAINT [DF_SessionHistory_PartitionID] DEFAULT ((0)) NOT NULL,
    [UserProfileID]    INT              NULL,
    [RoleID]           INT              NULL,
    [LoginDate]        DATETIME         NOT NULL,
    [UserName]         VARCHAR (50)     NULL,
    [Password]         VARCHAR (50)     NULL,
    [WorkstationName]  VARCHAR (50)     NULL,
    [IPAddress]        VARCHAR (50)     NULL,
    [ApplicationName]  VARCHAR (50)     NULL,
    CONSTRAINT [PK_SessionHistory] PRIMARY KEY CLUSTERED ([SessionHistoryID] ASC),
    CONSTRAINT [FK_SessionHistory_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([RoleID]),
    CONSTRAINT [FK_SessionHistory_UserProfile] FOREIGN KEY ([UserProfileID]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);

