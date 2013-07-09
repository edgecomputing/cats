CREATE TABLE [dbo].[Adjustment] (
    [AdjustmentID]        UNIQUEIDENTIFIER CONSTRAINT [DF_Adjustment_AdjustmentID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [PartitionID]         INT              NOT NULL,
    [TransactionGroupID]  UNIQUEIDENTIFIER NULL,
    [HubID]               INT              NOT NULL,
    [AdjustmentReasonID]  INT              NOT NULL,
    [AdjustmentDirection] CHAR (1)         NOT NULL,
    [AdjustmentDate]      DATETIME         NOT NULL,
    [ApprovedBy]          NVARCHAR (50)    NOT NULL,
    [Remarks]             NVARCHAR (500)   NULL,
    [UserProfileID]       INT              NOT NULL,
    [ReferenceNumber]     NVARCHAR (50)    NOT NULL,
    [StoreManName]        NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Adjustment_1] PRIMARY KEY CLUSTERED ([AdjustmentID] ASC),
    CONSTRAINT [FK_Adjustment_AdjustmentReason] FOREIGN KEY ([AdjustmentReasonID]) REFERENCES [dbo].[AdjustmentReason] ([AdjustmentReasonID]),
    CONSTRAINT [FK_Adjustment_TransactionGroup] FOREIGN KEY ([TransactionGroupID]) REFERENCES [dbo].[TransactionGroup] ([TransactionGroupID]),
    CONSTRAINT [FK_Adjustment_UserProfile] FOREIGN KEY ([UserProfileID]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The partition ID that this transaction occured on.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Adjustment', @level2type = N'COLUMN', @level2name = N'PartitionID';

