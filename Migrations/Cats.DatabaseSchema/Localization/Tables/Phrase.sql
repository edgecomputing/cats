CREATE TABLE [Localization].[Phrase] (
    [PhraseId]   INT            IDENTITY (1, 1) NOT NULL,
    [PhraseText] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Phrase] PRIMARY KEY CLUSTERED ([PhraseId] ASC),
    CONSTRAINT [UK_Phrase_PhraseText] UNIQUE NONCLUSTERED ([PhraseId] ASC)
);

