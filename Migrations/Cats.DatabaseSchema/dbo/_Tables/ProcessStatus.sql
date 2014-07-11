CREATE TABLE [dbo].[ProcessStatus] (
    [ProcessStatusID]   INT           IDENTITY (1, 1) NOT NULL,
    [ProcessTemplateID] INT           NOT NULL,
    [PerformedBy]       INT           NOT NULL,
    [PerformedDate]     DATE          NULL,
    [Comment]           NVARCHAR (50) NULL
);

