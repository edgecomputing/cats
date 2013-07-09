CREATE TABLE [dbo].[TransactionGroup] (
    [TransactionGroupID] UNIQUEIDENTIFIER CONSTRAINT [DF_TransactionGroup_TransactionGroupID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [PartitionID]        INT              NOT NULL,
    CONSTRAINT [PK_TransactionGroup_1] PRIMARY KEY CLUSTERED ([TransactionGroupID] ASC)
);

