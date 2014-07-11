CREATE TABLE [dbo].[AllocationRequest] (
    [AllocationRequestID] INT           IDENTITY (1, 1) NOT NULL,
    [Year]                INT           NULL,
    [Round]               INT           NULL,
    [RegionID]            INT           NULL,
    [ReferenceNumber]     NVARCHAR (50) NULL,
    [DateRequested]       DATETIME      NULL,
    [CreatedBy]           NVARCHAR (50) NULL,
    [DateCreated]         DATETIME      NULL,
    CONSTRAINT [PK_ReliefFoodAllocationRequest] PRIMARY KEY CLUSTERED ([AllocationRequestID] ASC)
);

