CREATE TABLE [dbo].[LetterTemplate] (
    [LetterTemplateID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50) NOT NULL,
    [Parameters]       XML           NULL,
    [Template]         NTEXT         NOT NULL,
    CONSTRAINT [PK_LetterTemplate] PRIMARY KEY CLUSTERED ([LetterTemplateID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'At the end of recording a gift certificate, the user might be interested in printing out a specific letter to the ministry of finance. these letter templates are stored in this letter templates table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LetterTemplate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Auto numbered primary key field for the letter template table', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LetterTemplate', @level2type = N'COLUMN', @level2name = N'LetterTemplateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the letter template', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LetterTemplate', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'an xml representation of the letter template parameters that will be replaced by the mail merege module.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LetterTemplate', @level2type = N'COLUMN', @level2name = N'Parameters';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The text template that gets printed in a mail merge operation on the gift certificate screen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LetterTemplate', @level2type = N'COLUMN', @level2name = N'Template';

