CREATE TABLE [dbo].[LetterTemplate_] (
    [LetterTemplateID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (500) NULL,
    [FileName]         NVARCHAR (500) NULL,
    CONSTRAINT [PK_LetterTemplate2] PRIMARY KEY CLUSTERED ([LetterTemplateID] ASC)
);

