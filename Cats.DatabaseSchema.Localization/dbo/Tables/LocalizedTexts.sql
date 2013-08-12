CREATE TABLE [dbo].[LocalizedTexts] (
    [LocalizedTextId] INT            IDENTITY (1, 1) NOT NULL,
    [LanguageCode]    NCHAR (2)      NOT NULL,
    [TextKey]         NVARCHAR (200) NULL,
    [TranslatedText]  NVARCHAR (200) NULL,
    CONSTRAINT [PK_LocalizedTexts] PRIMARY KEY CLUSTERED ([LocalizedTextId] ASC)
);





