CREATE TABLE [Procurement].[BidDetail] (
    [BidDetailID]            INT             IDENTITY (1, 1) NOT NULL,
    [BidID]                  INT             NOT NULL,
    [RegionID]               INT             NOT NULL,
    [AmountForReliefProgram] DECIMAL (19, 5) NULL,
    [AmountForPSNPProgram]   DECIMAL (19, 5) NULL,
    [BidDocumentPrice]       MONEY           NOT NULL,
    [CPO]                    MONEY           NOT NULL,
    CONSTRAINT [PK_BidDetail] PRIMARY KEY CLUSTERED ([BidDetailID] ASC),
    CONSTRAINT [FK_BidDetail_AdminUnit] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_BidDetail_Bid] FOREIGN KEY ([BidID]) REFERENCES [dbo].[Bid] ([BidID])
);

