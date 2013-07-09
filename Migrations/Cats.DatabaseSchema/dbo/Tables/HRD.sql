CREATE TABLE [dbo].[HRD] (
    [HRDID]            INT      IDENTITY (1, 1) NOT NULL,
    [Year]             INT      NULL,
    [CreatedBy]        INT      NULL,
    [RevisionNumber]   INT      NULL,
    [DateCreated]      DATETIME NULL,
    [SeasonID]         INT      NULL,
    [IsWorkingVersion] BIT      NULL,
    [IsPublished]      BIT      NULL,
    [PublishedDate]    DATETIME NULL,
    [RationID]         INT      NULL,
    CONSTRAINT [PK_HRD] PRIMARY KEY CLUSTERED ([HRDID] ASC),
    CONSTRAINT [FK_HRD_RationRate] FOREIGN KEY ([RationID]) REFERENCES [dbo].[Ration] ([RationID]),
    CONSTRAINT [FK_HRD_Season] FOREIGN KEY ([SeasonID]) REFERENCES [dbo].[Season] ([SeasonID]),
    CONSTRAINT [FK_HRD_UserProfile] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);

