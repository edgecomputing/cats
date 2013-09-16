CREATE TABLE [dbo].[Detail] (
    [DetailID]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (500) NULL,
    [MasterID]    INT            NOT NULL,
    [SortOrder]   INT            NOT NULL,
    CONSTRAINT [PK_Detail] PRIMARY KEY CLUSTERED ([DetailID] ASC),
    CONSTRAINT [FK_Detail_Master] FOREIGN KEY ([MasterID]) REFERENCES [dbo].[Master] ([MasterID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'In the Master - Detail relationship, this is the detail table which should contain detailed lookup values that are remotely needed in the commoditiy tracking system', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key for the detail table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Detail', @level2type = N'COLUMN', @level2name = N'DetailID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name or the main entry of the detail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Detail', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A longer description for the detail lookup value. when the name field contains abbreviations, please use this field to write the longer version', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Detail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID of the Master, in the master detail relationship.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Detail', @level2type = N'COLUMN', @level2name = N'MasterID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Sort order, for UI listing purposes. when this table is being used in scenarios that depend on non alphabetic sorting order, this field could be used to customize the sort order.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Detail', @level2type = N'COLUMN', @level2name = N'SortOrder';

