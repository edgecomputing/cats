CREATE TABLE [dbo].[AidDistributionPlanDetail] (
    [AidDistributionPlanDetailID] INT IDENTITY (1, 1) NOT NULL,
    [AidDistributionPlanID]       INT NOT NULL,
    [AdminUnitID]                 INT NOT NULL,
    [StartingMonth]               INT NOT NULL,
    [StartingRound]               INT NOT NULL,
    [Duration]                    INT NOT NULL,
    CONSTRAINT [PK_AidDistributionPlanRequestDetail] PRIMARY KEY CLUSTERED ([AidDistributionPlanDetailID] ASC),
    CONSTRAINT [FK_AidDistributionPlanDetail_AdminUnit] FOREIGN KEY ([AdminUnitID]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID])
);

