CREATE TABLE [dbo].[CommodityDistributionPlan] (
    [CommodityDistributionPlanID] INT              IDENTITY (1, 1) NOT NULL,
    [CommodityID]                 INT              NULL,
    [Amount]                      FLOAT (53)       NULL,
    [rowguid]                     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_CommodityDistributionPlan] PRIMARY KEY CLUSTERED ([CommodityDistributionPlanID] ASC),
    CONSTRAINT [FK_CommodityDistributionPlan_Commodity] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID])
);

