CREATE TABLE [dbo].[Ledger] (
    [LedgerID]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [LedgerTypeID] INT           NOT NULL,
    CONSTRAINT [PK_Ledger] PRIMARY KEY CLUSTERED ([LedgerID] ASC),
    CONSTRAINT [FK_Ledger_LedgerType] FOREIGN KEY ([LedgerTypeID]) REFERENCES [dbo].[LedgerType] ([LedgerTypeID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The core of the CTS, the commodity tracking is constructed off a double entry transactional table. The ledger table is a representative of the ledgers involved in the transactions table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Ledger';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The primary key of the transactions table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Ledger', @level2type = N'COLUMN', @level2name = N'LedgerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the ledger', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Ledger', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the Ledger. (Is it one that works to the positive ( Debit ) ? or is it one that works to the negative(Credit/Liability)?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Ledger', @level2type = N'COLUMN', @level2name = N'LedgerTypeID';

