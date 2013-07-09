CREATE TABLE [dbo].[DistributionPlan] (
    [DistributionPlanID] INT      IDENTITY (1, 1) NOT NULL,
    [Year]               INT      NOT NULL,
    [Round]              INT      NOT NULL,
    [Region]             INT      NULL,
    [CreatedBy]          INT      NULL,
    [DateCreated]        DATETIME NOT NULL,
    [IsDraft]            BIT      CONSTRAINT [DF_DistrubutionPlan_IsDraft] DEFAULT ((1)) NOT NULL,
    [HRDID]              INT      NOT NULL,
    [ProgramID]          INT      NOT NULL,
    [Month]              INT      NOT NULL,
    [RationID]           INT      NOT NULL,
    CONSTRAINT [PK_ReliefAllocation] PRIMARY KEY CLUSTERED ([DistributionPlanID] ASC),
    CONSTRAINT [FK_DistributionPlan_AdminUnit] FOREIGN KEY ([Region]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_DistrubutionPlan_HRD] FOREIGN KEY ([HRDID]) REFERENCES [dbo].[HRD] ([HRDID]),
    CONSTRAINT [FK_DistrubutionPlan_UserProfile] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);

