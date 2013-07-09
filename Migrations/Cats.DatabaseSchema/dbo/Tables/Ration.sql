CREATE TABLE [dbo].[Ration] (
    [RationID]    INT            IDENTITY (1, 1) NOT NULL,
    [ProgramID]   INT            NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [CreatedDate] DATETIME       NULL,
    [CreatedBy]   INT            NULL,
    CONSTRAINT [PK_Ration] PRIMARY KEY CLUSTERED ([RationID] ASC),
    CONSTRAINT [FK_RationRate_UserProfile] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);

