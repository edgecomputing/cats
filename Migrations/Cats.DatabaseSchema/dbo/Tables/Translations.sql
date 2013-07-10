CREATE TABLE [dbo].[Translations] (
    [TranslationID]  INT            IDENTITY (1, 1) NOT NULL,
    [LanguageCode]   NVARCHAR (MAX) NULL,
    [Phrase]         NVARCHAR (MAX) NULL,
    [TranslatedText] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Translations] PRIMARY KEY CLUSTERED ([TranslationID] ASC)
);

