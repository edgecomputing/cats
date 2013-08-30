CREATE TABLE [dbo].[HRD] (
    [HRDID]            INT      IDENTITY (1, 1) NOT NULL,
    [Year]             INT      NULL,
    [CreatedBy]        INT      NULL,
    [RevisionNumber]   INT      NULL,
    [DateCreated]      DATETIME NULL,
    [Month]            INT      NULL,
    [IsWorkingVersion] BIT      NULL,
    [IsPublished]      BIT      NULL,
    [PublishedDate]    DATETIME NULL,
    [RationID]         INT      NULL,
    [NeedAssessmentID] INT      NULL,
    CONSTRAINT [PK_HRD] PRIMARY KEY CLUSTERED ([HRDID] ASC),
    CONSTRAINT [FK_HRD_UserProfile] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);

