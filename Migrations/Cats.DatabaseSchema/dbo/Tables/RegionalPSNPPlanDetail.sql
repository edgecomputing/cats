CREATE TABLE [dbo].[RegionalPSNPPlanDetail] (
    [RegionalPSNPPlanDetailID] INT IDENTITY (1, 1) NOT NULL,
    [RegionalPSNPPlanID]       INT NOT NULL,
    [PlanedFDPID]              INT NULL,
    [BeneficiaryCount]         INT NULL,
    [FoodRatio]                INT NULL,
    [CashRatio]                INT NULL,
    [Item3Ratio]               INT NULL,
    [Item4Ratio]               INT NULL,
    CONSTRAINT [PK_RegionalPSNPPlanDetail] PRIMARY KEY CLUSTERED ([RegionalPSNPPlanDetailID] ASC)
);

