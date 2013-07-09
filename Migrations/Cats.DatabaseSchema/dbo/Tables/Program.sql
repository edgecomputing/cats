CREATE TABLE [dbo].[Program] (
    [ProgramID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (50)  NULL,
    [LongName]    NVARCHAR (500) NULL,
    CONSTRAINT [PK_Program] PRIMARY KEY CLUSTERED ([ProgramID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Collection of Programmes in the DRMFSS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Program';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key of the program table in the application.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Program', @level2type = N'COLUMN', @level2name = N'ProgramID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the progarm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Program', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A short descritpion of the program in the list.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Program', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'If the program is an abbreviation, the expanded name of the program in the list.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Program', @level2type = N'COLUMN', @level2name = N'LongName';

