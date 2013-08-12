CREATE TABLE [EarlyWarning].[RequestDetailCommodity] (
    [RequestCommodityID]      INT          IDENTITY (1, 1) NOT NULL,
    [RegionalRequestDetailID] INT          NOT NULL,
    [CommodityID]             INT          NOT NULL,
    [Amount]                  DECIMAL (18) NOT NULL,
    [UnitID]                  INT          NULL,
    CONSTRAINT [PK_RequestDetailCommodity] PRIMARY KEY CLUSTERED ([RequestCommodityID] ASC),
    CONSTRAINT [FK_RequestDetailCommodity_Commodity] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_RequestDetailCommodity_RegionalRequestDetail] FOREIGN KEY ([RegionalRequestDetailID]) REFERENCES [EarlyWarning].[RegionalRequestDetail] ([RegionalRequestDetailID]),
    CONSTRAINT [IX_RequestDetailCommodity] UNIQUE NONCLUSTERED ([RegionalRequestDetailID] ASC, [CommodityID] ASC)
);



