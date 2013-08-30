CREATE TABLE [dbo].[ReceiveDetail] (
    [ReceiveDetailID]    UNIQUEIDENTIFIER CONSTRAINT [DF_ReceiveDetail_ReceiveDetailID] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [PartitionID]        INT              NOT NULL,
    [ReceiveID]          UNIQUEIDENTIFIER NULL,
    [TransactionGroupID] UNIQUEIDENTIFIER NULL,
    [CommodityID]        INT              NOT NULL,
    [SentQuantityInUnit] DECIMAL (18, 3)  NOT NULL,
    [UnitID]             INT              NOT NULL,
    [SentQuantityInMT]   DECIMAL (18, 3)  NOT NULL,
    [Description]        NVARCHAR (500)   NULL,
    CONSTRAINT [PK_ReceiveDetail_1] PRIMARY KEY CLUSTERED ([ReceiveDetailID] ASC),
    CONSTRAINT [FK_ReceiveCommodity_Commodity] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_ReceiveCommodity_Unit] FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Unit] ([UnitID]),
    CONSTRAINT [FK_ReceiveDetail_Receive1] FOREIGN KEY ([ReceiveID]) REFERENCES [dbo].[Receive] ([ReceiveID]),
    CONSTRAINT [FK_ReceiveDetail_TransactionGroup] FOREIGN KEY ([TransactionGroupID]) REFERENCES [dbo].[TransactionGroup] ([TransactionGroupID])
);

