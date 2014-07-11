CREATE TABLE [dbo].[UserProfile] (
    [UserAccountId]   INT            NOT NULL,
    [FirstName]       NVARCHAR (200) NOT NULL,
    [LastName]        NVARCHAR (200) NOT NULL,
    [GrandFatherName] NVARCHAR (200) NOT NULL,
    [Email]           NVARCHAR (50)  NULL,
    [CaseTeam]        INT            NULL,
    CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED ([UserAccountId] ASC),
    CONSTRAINT [FK_UserProfile_UserAccount] FOREIGN KEY ([UserAccountId]) REFERENCES [dbo].[UserAccount] ([UserAccountId])
);





