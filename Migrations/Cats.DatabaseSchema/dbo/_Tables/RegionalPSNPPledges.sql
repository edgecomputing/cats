CREATE TABLE [dbo].[RegionalPSNPPledges] (
    [RegionalPSNPPledgeID]     INT             IDENTITY (1, 1) NOT NULL,
    [RegionalPSNPPlanID] INT             NOT NULL,
    [DonorID]                  INT             NOT NULL,
    [CommodityID]              INT             NOT NULL,
    [Quantity]                 DECIMAL (18, 6) NOT NULL,
    [UnitID]                   INT             NOT NULL,
    [PledgeDate]               DATETIME        NOT NULL,
    CONSTRAINT [PK_RegionalPSNPPledgess] PRIMARY KEY CLUSTERED ([RegionalPSNPPledgeID] ASC),
    CONSTRAINT [FK_RegionalPSNPPledgess_Commodity] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_RegionalPSNPPledgess_Donor] FOREIGN KEY ([DonorID]) REFERENCES [dbo].[Donor] ([DonorID]),
    CONSTRAINT [FK_RegionalPSNPPledgess_RegionalPSNPPlan] FOREIGN KEY ([RegionalPSNPPlanID]) REFERENCES [dbo].[RegionalPSNPPlan] ([RegionalPSNPPlanID]),
    CONSTRAINT [FK_RegionalPSNPPledgess_Unit] FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Unit] ([UnitID])
);



