CREATE TABLE [dbo].[Bid] (
    [BidID]       INT            IDENTITY (1, 1) NOT NULL,
    [StartDate]   DATETIME       NOT NULL,
    [EndDate]     DATETIME       NOT NULL,
    [BidNumber]   NVARCHAR (200) NOT NULL,
    [OpeningDate] DATETIME       NULL,
    [StatusID]    INT            NULL,
    CONSTRAINT [PK_Bid] PRIMARY KEY CLUSTERED ([BidID] ASC)
);

