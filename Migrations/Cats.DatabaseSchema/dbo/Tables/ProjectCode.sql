CREATE TABLE [dbo].[ProjectCode] (
    [ProjectCodeID] INT           IDENTITY (1, 1) NOT NULL,
    [Value]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ProjectCode] PRIMARY KEY CLUSTERED ([ProjectCodeID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProjectCode]
    ON [dbo].[ProjectCode]([Value] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Project code is an internal batching system the DRMFSS uses to identify allocated receitps. It is a little ambiguious at this point. however, this is a table that racks the coding system used at the hubs.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProjectCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'An integer Auto numbered primary key field.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProjectCode', @level2type = N'COLUMN', @level2name = N'ProjectCodeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Textual value of the project code.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProjectCode', @level2type = N'COLUMN', @level2name = N'Value';

