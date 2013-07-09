CREATE TABLE [dbo].[Partition] (
    [PartitionID]          INT            IDENTITY (1, 1) NOT NULL,
    [HubID]                INT            NOT NULL,
    [ServerUserName]       NVARCHAR (500) NOT NULL,
    [PartitionCreatedDate] DATETIME       CONSTRAINT [DF_Partition_PartitionCreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastUpdated]          DATETIME       NULL,
    [LastSyncTime]         DATETIME       NULL,
    [HasConflict]          BIT            CONSTRAINT [DF_Partition_HasConflict] DEFAULT ((0)) NOT NULL,
    [IsActive]             BIT            CONSTRAINT [DF_Partition_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Partition] PRIMARY KEY CLUSTERED ([PartitionID] ASC)
);

