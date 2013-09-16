CREATE TABLE [dbo].[AidDistributionPlan] (
    [AidDistributionPlanID] INT           IDENTITY (1, 1) NOT NULL,
    [OrganisationID]        INT           NULL,
    [HRDID]                 INT           NULL,
    [ReferenceNumber]       NVARCHAR (50) NOT NULL,
    [DateRequested]         DATETIME      NOT NULL,
    [IsApproved]            BIT           NOT NULL,
    CONSTRAINT [PK_AidDistributionPlanRequest] PRIMARY KEY CLUSTERED ([AidDistributionPlanID] ASC),
    CONSTRAINT [FK_AidDistributionPlan_HRD] FOREIGN KEY ([HRDID]) REFERENCES [dbo].[HRD] ([HRDID]),
    CONSTRAINT [FK_AidDistributionPlan_Organisation] FOREIGN KEY ([OrganisationID]) REFERENCES [dbo].[Organisation] ([OrganisationID])
);

