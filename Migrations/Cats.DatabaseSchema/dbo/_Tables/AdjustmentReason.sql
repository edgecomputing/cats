CREATE TABLE [dbo].[AdjustmentReason] (
    [AdjustmentReasonID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (50) NULL,
    [Direction]          CHAR (1)      CONSTRAINT [DF_AdjustmentReason_Direction] DEFAULT ('+') NOT NULL,
    CONSTRAINT [PK_AdjustmentReason] PRIMARY KEY CLUSTERED ([AdjustmentReasonID] ASC)
);

