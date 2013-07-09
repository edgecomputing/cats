CREATE TABLE [dbo].[InternalMovement] (
    [InternalMovementID] UNIQUEIDENTIFIER CONSTRAINT [DF_InternalMovement_InternalMovementID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [PartitionID]        INT              CONSTRAINT [DF_InternalMovement_PartitionID] DEFAULT ((0)) NOT NULL,
    [HubID]              INT              NOT NULL,
    [TransactionGroupID] UNIQUEIDENTIFIER NULL,
    [TransferDate]       DATETIME         NOT NULL,
    [ReferenceNumber]    NVARCHAR (50)    NOT NULL,
    [DReason]            INT              NOT NULL,
    [Notes]              NVARCHAR (4000)  NULL,
    [ApprovedBy]         NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_InternalMovement] PRIMARY KEY CLUSTERED ([InternalMovementID] ASC),
    CONSTRAINT [FK_InternalMovement_Detail] FOREIGN KEY ([DReason]) REFERENCES [dbo].[Detail] ([DetailID]),
    CONSTRAINT [FK_InternalMovement_TransactionGroup] FOREIGN KEY ([TransactionGroupID]) REFERENCES [dbo].[TransactionGroup] ([TransactionGroupID])
);

