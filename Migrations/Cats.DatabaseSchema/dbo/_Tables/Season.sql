CREATE TABLE [dbo].[Season] (
    [SeasonID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) NULL,
    CONSTRAINT [PK_Season] PRIMARY KEY CLUSTERED ([SeasonID] ASC)
);

