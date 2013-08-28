CREATE TABLE [EarlyWarning].[Season] (
    [SeasonID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Season] PRIMARY KEY CLUSTERED ([SeasonID] ASC),
    CONSTRAINT [IX_Season] UNIQUE NONCLUSTERED ([Name] ASC)
);

