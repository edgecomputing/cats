CREATE TABLE [dbo].[User] (
    [UserId]   INT            IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (200) NOT NULL,
    [FullName] NVARCHAR (200) NOT NULL,
    [Email]    NVARCHAR (50)  NULL,
    [Password] NVARCHAR (MAX) NOT NULL,
    [Disabled] BIT            CONSTRAINT [DF_User_Disabled] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

