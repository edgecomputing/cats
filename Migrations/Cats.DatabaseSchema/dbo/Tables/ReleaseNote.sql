CREATE TABLE [dbo].[ReleaseNote] (
    [ReleaseNoteID] INT           IDENTITY (1, 1) NOT NULL,
    [ReleaseName]   NVARCHAR (50) NULL,
    [BuildNumber]   INT           NULL,
    [Date]          DATETIME      CONSTRAINT [DF_ReleaseNote_Date] DEFAULT (getdate()) NOT NULL,
    [Notes]         TEXT          NOT NULL,
    [Details]       TEXT          NULL,
    [DBuildQuality] INT           NOT NULL,
    [Comments]      TEXT          NULL,
    CONSTRAINT [PK_ReleaseNote] PRIMARY KEY CLUSTERED ([ReleaseNoteID] ASC)
);

