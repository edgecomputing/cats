CREATE TABLE [dbo].[Languages] (
    [LanguageID]   INT            IDENTITY (1, 1) NOT NULL,
    [LanguageCode] NCHAR (2)      NULL,
    [Name]         NVARCHAR (100) NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([LanguageID] ASC),
    CONSTRAINT [IX_Languages] UNIQUE NONCLUSTERED ([LanguageCode] ASC)
);

