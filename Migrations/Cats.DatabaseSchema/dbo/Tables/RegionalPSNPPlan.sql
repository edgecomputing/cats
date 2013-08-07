CREATE TABLE [dbo].[RegionalPSNPPlan] (
    [RegionalPSNPPlanID] INT IDENTITY (1, 1) NOT NULL,
    [Year]               INT NULL,
    [Duration]           INT NULL,
    [RegionID]           INT NULL,
    CONSTRAINT [PK_RegionalPSNPPlan] PRIMARY KEY CLUSTERED ([RegionalPSNPPlanID] ASC)
);

