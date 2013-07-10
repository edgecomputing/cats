CREATE TABLE [Procurement].[TransportBidPlanDetail] (
    [TransportBidPlanDetailID] INT          IDENTITY (1, 1) NOT NULL,
    [DestinationID]            INT          NOT NULL,
    [SourceID]                 INT          NOT NULL,
    [ProgramID]                INT          NOT NULL,
    [BidPlanID]                INT          NOT NULL,
    [Quantity]                 DECIMAL (18) NULL
);

