CREATE TABLE [dbo].[DistributionBy] (
    [DistributionByID] INT           IDENTITY (1, 1) NOT NULL,
    [DistributedBy]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_DistributionBy] PRIMARY KEY CLUSTERED ([DistributionByID] ASC)
);

