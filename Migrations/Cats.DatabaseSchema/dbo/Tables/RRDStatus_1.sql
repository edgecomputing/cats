CREATE TABLE [dbo].[RRDStatus] (
    [RRDStatusID] INT          NOT NULL,
    [Status]      VARCHAR (50) NULL,
    CONSTRAINT [PK_RRDStatus] PRIMARY KEY CLUSTERED ([RRDStatusID] ASC)
);

