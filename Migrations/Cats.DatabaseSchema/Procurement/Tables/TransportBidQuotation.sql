CREATE TABLE [Procurement].[TransportBidQuotation] (
    [TransportBidQuotationID] INT           IDENTITY (1, 1) NOT NULL,
    [BidID]                   INT           NULL,
    [TransporterID]           INT           NULL,
    [SourceID]                INT           NULL,
    [DestinationID]           INT           NULL,
    [Tariff]                  DECIMAL (18)  NULL,
    [IsWinner]                BIT           NULL,
    [Position]                INT           NULL,
    [Remark]                  NVARCHAR (50) NULL
);

