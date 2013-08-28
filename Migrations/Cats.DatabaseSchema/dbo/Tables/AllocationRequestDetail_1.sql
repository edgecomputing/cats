CREATE TABLE [dbo].[AllocationRequestDetail] (
    [AllocationRequestDetailID] INT IDENTITY (1, 1) NOT NULL,
    [AllocationRequestID]       INT NOT NULL,
    [FDPID]                     INT NULL,
    [Beneficiaries]             INT NULL,
    CONSTRAINT [PK_ReliefFoodAllocationRequestDetail] PRIMARY KEY CLUSTERED ([AllocationRequestDetailID] ASC),
    CONSTRAINT [FK_AllocationRequestDetail_FDP] FOREIGN KEY ([FDPID]) REFERENCES [dbo].[FDP] ([FDPID]),
    CONSTRAINT [FK_ReliefFoodAllocationRequestDetail_ReliefFoodAllocationRequest] FOREIGN KEY ([AllocationRequestID]) REFERENCES [dbo].[AllocationRequest] ([AllocationRequestID])
);

