CREATE TABLE [dbo].[WorkflowStatus] (
    [StatusID]    INT           NOT NULL,
    [WorkflowID]  INT           NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_WorkflowStatus] PRIMARY KEY CLUSTERED ([StatusID] ASC, [WorkflowID] ASC),
    CONSTRAINT [FK_WorkflowStatus_Workflow] FOREIGN KEY ([WorkflowID]) REFERENCES [dbo].[Workflow] ([WorkflowID])
);







