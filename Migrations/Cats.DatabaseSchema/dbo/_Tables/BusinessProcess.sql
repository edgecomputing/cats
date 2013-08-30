CREATE TABLE [dbo].[BusinessProcess] (
    [BusinessProcessID] INT           IDENTITY (1, 1) NOT NULL,
    [ProcessTypeID]     INT           NULL,
    [DocumentID]        INT           NULL,
    [DocumentType]      NVARCHAR (50) NULL,
    [CurrentStateID]    INT           NULL,
    CONSTRAINT [PK_BusinessProcess] PRIMARY KEY CLUSTERED ([BusinessProcessID] ASC)
);

