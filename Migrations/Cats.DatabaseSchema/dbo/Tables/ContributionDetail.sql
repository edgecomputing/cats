CREATE TABLE [dbo].[ContributionDetail] (
    [ContributionDetailID] INT           IDENTITY (1, 1) NOT NULL,
    [ContributiionID]      INT           NOT NULL,
    [CommodityID]          INT           NOT NULL,
    [PledgeReferenceNo]    NVARCHAR (50) NULL,
    [PledgeDate]           DATETIME      NULL,
    [Quantity]             DECIMAL (18)  NOT NULL,
    CONSTRAINT [PK_ContributionDetail] PRIMARY KEY CLUSTERED ([ContributionDetailID] ASC),
    CONSTRAINT [FK_ContributionDetail_Commodity] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_ContributionDetail_Contribution] FOREIGN KEY ([ContributiionID]) REFERENCES [dbo].[Contribution] ([ContributionID]),
    CONSTRAINT [FK_ContributionDetail_ContributionDetail] FOREIGN KEY ([ContributionDetailID]) REFERENCES [dbo].[ContributionDetail] ([ContributionDetailID])
);

