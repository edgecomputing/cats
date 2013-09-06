CREATE TABLE [dbo].[ContributionDetail] (
    [ContributionDetailID] INT           IDENTITY (1, 1) NOT NULL,
    [ContributionID]       INT           NOT NULL,
    [CurrencyID]           INT           NOT NULL,
    [PledgeReferenceNo]    NVARCHAR (50) NULL,
    [PledgeDate]           DATETIME      NULL,
    [Amount]               DECIMAL (18)  NOT NULL,
    CONSTRAINT [PK_ContributionDetail] PRIMARY KEY CLUSTERED ([ContributionDetailID] ASC),
    CONSTRAINT [FK_ContributionDetail_Contribution1] FOREIGN KEY ([ContributionID]) REFERENCES [dbo].[Contribution] ([ContributionID]),
    CONSTRAINT [FK_ContributionDetail_Currency1] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[Currency] ([CurrencyID])
);



