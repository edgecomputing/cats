CREATE TABLE [dbo].[DispatchDetail] (
    [DispatchDetailID]        UNIQUEIDENTIFIER CONSTRAINT [DF_DispatchDetail_DispatchDetailID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [PartitionID]             INT              NOT NULL,
    [TransactionGroupID]      UNIQUEIDENTIFIER NULL,
    [DispatchID]              UNIQUEIDENTIFIER NULL,
    [CommodityID]             INT              NOT NULL,
    [RequestedQunatityInUnit] DECIMAL (18, 3)  NOT NULL,
    [UnitID]                  INT              NOT NULL,
    [RequestedQuantityInMT]   DECIMAL (18, 3)  NOT NULL,
    [Description]             NVARCHAR (500)   NULL,
    CONSTRAINT [PK_DispatchDetail] PRIMARY KEY CLUSTERED ([DispatchDetailID] ASC),
    CONSTRAINT [FK_DispatchCommodity_Commodity] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_DispatchCommodity_Unit] FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Unit] ([UnitID]),
    CONSTRAINT [FK_DispatchDetail_Dispatch] FOREIGN KEY ([DispatchID]) REFERENCES [dbo].[Dispatch] ([DispatchID]),
    CONSTRAINT [FK_DispatchDetail_TransactionGroup] FOREIGN KEY ([TransactionGroupID]) REFERENCES [dbo].[TransactionGroup] ([TransactionGroupID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contains the detail list of commodities and quantity dispatched from a given dispach transaction. this doesn''t contain transportation detail.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchDetail';

