CREATE TABLE [dbo].[HRD] (
    [HRDID]            INT      IDENTITY (1, 1) NOT NULL,
    [Year]             INT      NULL,
    [CreatedBy]        INT      NULL,
    [RevisionNumber]   INT      NULL,
    [DateCreated]      DATETIME NULL,
    [SeasonID]         INT      NULL,
    [PublishedDate]    DATETIME NULL,
    [RationID]         INT      NULL,
    [NeedAssessmentID] INT      NULL,
    [Status]           INT      NULL,
    CONSTRAINT [PK_HRD] PRIMARY KEY CLUSTERED ([HRDID] ASC)
);





