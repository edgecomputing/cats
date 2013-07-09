CREATE TABLE [Logistics].[ProjectCodeAllocation] (
    [ProjectCodeAllocationID] INT      NOT NULL,
    [HubAllocationID]         INT      NOT NULL,
    [ProjectCodeID]           INT      NULL,
    [Amount_FromProject]      INT      NULL,
    [SINumberID]              INT      NULL,
    [Amount_FromSI]           INT      NULL,
    [AllocatedBy]             INT      NOT NULL,
    [AlloccationDate]         DATETIME NOT NULL,
    CONSTRAINT [PK_ProjectCodeAllocation] PRIMARY KEY CLUSTERED ([ProjectCodeAllocationID] ASC),
    CONSTRAINT [FK_ProjectCodeAllocation_HubAllocation] FOREIGN KEY ([HubAllocationID]) REFERENCES [Logistics].[HubAllocation] ([HubAllocationID]),
    CONSTRAINT [FK_ProjectCodeAllocation_ProjectCode] FOREIGN KEY ([ProjectCodeID]) REFERENCES [dbo].[ProjectCode] ([ProjectCodeID]),
    CONSTRAINT [FK_ProjectCodeAllocation_ShippingInstruction] FOREIGN KEY ([SINumberID]) REFERENCES [dbo].[ShippingInstruction] ([ShippingInstructionID]),
    CONSTRAINT [FK_ProjectCodeAllocation_UserProfile] FOREIGN KEY ([AllocatedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);

