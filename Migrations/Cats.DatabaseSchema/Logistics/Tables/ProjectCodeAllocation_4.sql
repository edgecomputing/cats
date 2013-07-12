CREATE TABLE [Logistics].[ProjectCodeAllocation] (
    [ProjectCodeAllocationID] INT      IDENTITY (1, 1) NOT NULL,
    [HubAllocationID]         INT      NOT NULL,
    [ProjectCodeID]           INT      NULL,
    [Amount_FromProject]      INT      NULL,
    [SINumberID]              INT      NULL,
    [Amount_FromSI]           INT      NULL,
    [AllocatedBy]             INT      NOT NULL,
    [AlloccationDate]         DATETIME NULL,
    CONSTRAINT [PK_ProjectCodeAllocation] PRIMARY KEY CLUSTERED ([ProjectCodeAllocationID] ASC),
    CONSTRAINT [FK_ProjectCodeAllocation_HubAllocation] FOREIGN KEY ([HubAllocationID]) REFERENCES [Logistics].[HubAllocation] ([HubAllocationID]) NOT FOR REPLICATION,
    CONSTRAINT [FK_ProjectCodeAllocation_ProjectCode] FOREIGN KEY ([ProjectCodeID]) REFERENCES [dbo].[ProjectCode] ([ProjectCodeID]) NOT FOR REPLICATION,
    CONSTRAINT [FK_ProjectCodeAllocation_ShippingInstruction] FOREIGN KEY ([SINumberID]) REFERENCES [dbo].[ShippingInstruction] ([ShippingInstructionID]) NOT FOR REPLICATION,
    CONSTRAINT [FK_ProjectCodeAllocation_UserProfile] FOREIGN KEY ([AllocatedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID]) NOT FOR REPLICATION
);


GO
ALTER TABLE [Logistics].[ProjectCodeAllocation] NOCHECK CONSTRAINT [FK_ProjectCodeAllocation_HubAllocation];


GO
ALTER TABLE [Logistics].[ProjectCodeAllocation] NOCHECK CONSTRAINT [FK_ProjectCodeAllocation_ProjectCode];


GO
ALTER TABLE [Logistics].[ProjectCodeAllocation] NOCHECK CONSTRAINT [FK_ProjectCodeAllocation_ShippingInstruction];


GO
ALTER TABLE [Logistics].[ProjectCodeAllocation] NOCHECK CONSTRAINT [FK_ProjectCodeAllocation_UserProfile];

