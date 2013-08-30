CREATE TABLE [Procurement].[Bid] (
    [BidID]     INT            IDENTITY (1, 1) NOT NULL,
    [StartDate] DATETIME       NOT NULL,
    [EndDate]   DATETIME       NOT NULL,
    [BidNumber] NVARCHAR (255) NOT NULL,
    [StatusID]  INT            NOT NULL,
    CONSTRAINT [PK_Bid] PRIMARY KEY CLUSTERED ([BidID] ASC),
    CONSTRAINT [FK_Bid_Status] FOREIGN KEY ([StatusID]) REFERENCES [Procurement].[Status] ([StatusID])
);



