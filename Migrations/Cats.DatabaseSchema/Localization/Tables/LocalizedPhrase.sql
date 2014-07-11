CREATE TABLE [Localization].[LocalizedPhrase] (
    [LocalizationId] INT            IDENTITY (1, 1) NOT NULL,
    [LanguageCode]   NCHAR (2)      NOT NULL,
    [PhraseId]       INT            NOT NULL,
    [TranslatedText] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_LocalizedPhrase] PRIMARY KEY CLUSTERED ([LocalizationId] ASC),
    CONSTRAINT [FK_LocalizedPhrase_Language] FOREIGN KEY ([LanguageCode]) REFERENCES [Localization].[Language] ([LanguageCode]),
    CONSTRAINT [FK_LocalizedPhrase_Phrase] FOREIGN KEY ([PhraseId]) REFERENCES [Localization].[Phrase] ([PhraseId])
);

