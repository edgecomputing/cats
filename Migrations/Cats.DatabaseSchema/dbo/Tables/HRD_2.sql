CREATE TABLE [dbo].[HRD] (
    [HRDID]          INT      IDENTITY (1, 1) NOT NULL,
    [Year]           INT      NOT NULL,
    [CreatedBy]      INT      NOT NULL,
    [RevisionNumber] INT      NULL,
    [DateCreated]    DATETIME NOT NULL,
    [SeasonID]       INT      NOT NULL,
    [PublishedDate]  DATETIME NULL,
    [RationID]       INT      NOT NULL,
    [Status]         INT      NOT NULL,
    CONSTRAINT [PK_HRD] PRIMARY KEY CLUSTERED ([HRDID] ASC)
);










GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HRD]
    ON [dbo].[HRD]([SeasonID] ASC, [Year] ASC);

