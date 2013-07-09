CREATE TABLE [dbo].[Master] (
    [MasterID]  INT           IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50) NOT NULL,
    [SortOrder] INT           NOT NULL,
    CONSTRAINT [PK_Master] PRIMARY KEY CLUSTERED ([MasterID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Master table is a list of detail categories of lookups that are not so much importatnt in the application other than being displayed on a few combo boxes in the appliction.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Master';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Auto numbered primary key of the mater table', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Master', @level2type = N'COLUMN', @level2name = N'MasterID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The name of the master lookup', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Master', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The sort order, if a custom sort order applyes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Master', @level2type = N'COLUMN', @level2name = N'SortOrder';

