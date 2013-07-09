CREATE TABLE [dbo].[Audit] (
    [AuditID]     UNIQUEIDENTIFIER CONSTRAINT [DF_Audit_AuditID] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [HubID]       INT              NULL,
    [PartitionID] INT              CONSTRAINT [DF_Audit_PartitionID] DEFAULT ((0)) NOT NULL,
    [LoginID]     INT              NOT NULL,
    [DateTime]    DATETIME         NOT NULL,
    [Action]      CHAR (1)         NOT NULL,
    [TableName]   VARCHAR (30)     NOT NULL,
    [PrimaryKey]  VARCHAR (50)     NULL,
    [ColumnName]  VARCHAR (3000)   NULL,
    [NewValue]    TEXT             NULL,
    [OldValue]    TEXT             NULL,
    CONSTRAINT [PK_Audit] PRIMARY KEY CLUSTERED ([AuditID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Audit Table holds a very long list of who changed what. this table makes the assumption that the primary key of the table is integer and that the application is the only code that modifies the data.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Audit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A reference to the User account who made the modification on the database.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Audit', @level2type = N'COLUMN', @level2name = N'LoginID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Date and Time this modification happened', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Audit', @level2type = N'COLUMN', @level2name = N'DateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Holds weather the action performed is Insert, Update, Edit or Delete.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Audit', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The name of the table that was affected by the current change', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Audit', @level2type = N'COLUMN', @level2name = N'TableName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The primary key of the record that was affected by the current change (Update/New/Delete operation)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Audit', @level2type = N'COLUMN', @level2name = N'PrimaryKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The column that was affected by the current change. this could be a comma separated list of fields', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Audit', @level2type = N'COLUMN', @level2name = N'ColumnName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'In case of Edit, the new value that was submitted.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Audit', @level2type = N'COLUMN', @level2name = N'NewValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'In case of Update, the Old value.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Audit', @level2type = N'COLUMN', @level2name = N'OldValue';

