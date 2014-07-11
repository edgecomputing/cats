CREATE TABLE [Localization].[Language] (
    [LanguageCode] NCHAR (2)     NOT NULL,
    [LanguageName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED ([LanguageCode] ASC)
);

