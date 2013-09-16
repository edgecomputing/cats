CREATE TABLE [dbo].[LedgerType] (
    [LedgerTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [Direction]    CHAR (1)      NOT NULL,
    CONSTRAINT [PK_LedgerType] PRIMARY KEY CLUSTERED ([LedgerTypeID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'In the double entry transactions system, this table represents the type of ledger we have and to which direction it runs,(to the positive? or the negative)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LedgerType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The ledger type id, Auto numbered primary key of the table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LedgerType', @level2type = N'COLUMN', @level2name = N'LedgerTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The name of the ledger type', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LedgerType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'direction of the ledger type (this is + or - ) value', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LedgerType', @level2type = N'COLUMN', @level2name = N'Direction';

