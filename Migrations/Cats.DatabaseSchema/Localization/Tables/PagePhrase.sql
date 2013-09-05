CREATE TABLE [Localization].[PagePhrase] (
    [PageId]   INT NOT NULL,
    [PhraseId] INT NOT NULL,
    CONSTRAINT [PK_PagePhrase] PRIMARY KEY CLUSTERED ([PageId] ASC, [PhraseId] ASC),
    CONSTRAINT [FK_PagePhrase_Page] FOREIGN KEY ([PageId]) REFERENCES [Localization].[Page] ([PageId]),
    CONSTRAINT [FK_PagePhrase_Phrase] FOREIGN KEY ([PhraseId]) REFERENCES [Localization].[Phrase] ([PhraseId])
);

