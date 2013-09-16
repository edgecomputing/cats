CREATE TABLE [dbo].[Translation] (
    [TranslationID]  INT             IDENTITY (1, 1) NOT NULL,
    [LanguageCode]   NVARCHAR (4)    NOT NULL,
    [Phrase]         NVARCHAR (4000) NOT NULL,
    [TranslatedText] NVARCHAR (4000) NOT NULL,
    CONSTRAINT [PK_Translation] PRIMARY KEY CLUSTERED ([TranslationID] ASC)
);

