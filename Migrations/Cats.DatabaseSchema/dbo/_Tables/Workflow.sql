CREATE TABLE [dbo].[Workflow] (
    [WorkflowID]  INT           NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Workflow] PRIMARY KEY CLUSTERED ([WorkflowID] ASC)
);

