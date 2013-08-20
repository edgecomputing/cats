CREATE TABLE [dbo].[FlowTemplate] (
    [FlowTemplateID] INT           IDENTITY (1, 1) NOT NULL,
    [InitialStateID] INT           NULL,
    [FinalStateID]   INT           NULL,
    [Name]           NVARCHAR (50) NULL
);

