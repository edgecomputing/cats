CREATE TABLE [dbo].[LocalizedTexts] (
    [LocalizedTextId] INT            IDENTITY (1, 1) NOT NULL,
    [LanguageCode]    NVARCHAR (10)  NULL,
    [TextKey]         NVARCHAR (200) NULL,
    [Value]           NVARCHAR (200) NULL
);

