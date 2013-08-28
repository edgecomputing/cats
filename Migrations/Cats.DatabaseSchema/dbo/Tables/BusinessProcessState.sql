CREATE TABLE [dbo].[BusinessProcessState] (
    [BusinessProcessStateID]  INT            IDENTITY (1, 1) NOT NULL,
    [ParentBusinessProcessID] INT            NULL,
    [StateID]                 INT            NULL,
    [PerformedBy]             NVARCHAR (50)  NULL,
    [DatePerformed]           DATETIME       NULL,
    [Comment]                 NVARCHAR (200) NULL,
    CONSTRAINT [PK_BusinessProcessState] PRIMARY KEY CLUSTERED ([BusinessProcessStateID] ASC)
);

