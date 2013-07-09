CREATE TABLE [dbo].[Role] (
    [RoleID]      INT          IDENTITY (1, 1) NOT NULL,
    [SortOrder]   INT          NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [Description] VARCHAR (50) NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);

