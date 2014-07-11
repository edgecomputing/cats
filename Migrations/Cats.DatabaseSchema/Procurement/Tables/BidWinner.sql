CREATE TABLE [Procurement].[BidWinner] (
    [BidWinnerID]      INT          NOT NULL,
    [BidID]            INT          NOT NULL,
    [TransportOrderID] INT          NULL,
    [SourceID]         INT          NOT NULL,
    [DestinationID]    INT          NOT NULL,
    [CommodityID]      INT          NULL,
    [TransporterID]    INT          NOT NULL,
    [Amount]           DECIMAL (18) NULL,
    [Tariff]           MONEY        NULL,
    [Position]         INT          NULL,
    [Status]           INT          NULL,
    [expiryDate]       DATETIME     NULL,
    CONSTRAINT [PK_BidWinner] PRIMARY KEY CLUSTERED ([BidWinnerID] ASC),
    CONSTRAINT [FK_BidWinner_Bid] FOREIGN KEY ([BidWinnerID]) REFERENCES [dbo].[Bid] ([BidID]),
    CONSTRAINT [FK_BidWinner_Commodity] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_BidWinner_Transporter] FOREIGN KEY ([TransportOrderID]) REFERENCES [Procurement].[Transporter] ([TransporterID])
);



