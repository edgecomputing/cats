CREATE TABLE [dbo].[AccountTransaction] (
    [AccountTransactionID]  UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [PartitionID]           INT              NULL,
    [TransactionGroupID]    UNIQUEIDENTIFIER NOT NULL,
    [LedgerID]              INT              NOT NULL,
    [HubOwnerID]            INT              NULL,
    [AccountID]             INT              NULL,
    [HubID]                 INT              NULL,
    [StoreID]               INT              NULL,
    [Stack]                 INT              NULL,
    [ProjectCodeID]         INT              NULL,
    [ShippingInstructionID] INT              NULL,
    [ProgramID]             INT              NOT NULL,
    [ParentCommodityID]     INT              NULL,
    [CommodityID]           INT              NOT NULL,
    [CommodityGradeID]      INT              NULL,
    [QuantityInMT]          DECIMAL (18, 3)  NOT NULL,
    [QuantityInUnit]        DECIMAL (18, 3)  NOT NULL,
    [UnitID]                INT              NOT NULL,
    [TransactionDate]       DATETIME         NOT NULL,
    [RegionID]              INT              NULL,
    [Month]                 INT              NULL,
    [Round]                 INT              NULL,
    [DonorID]               INT              NULL,
    [CommoditySourceID]     INT              NULL,
    [GiftTypeID]            INT              NULL
);



