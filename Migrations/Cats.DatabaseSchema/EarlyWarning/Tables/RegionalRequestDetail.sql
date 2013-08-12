CREATE TABLE [EarlyWarning].[RegionalRequestDetail] (
    [RegionalRequestDetailID] INT IDENTITY (1, 1) NOT NULL,
    [RegionalRequestID]       INT NOT NULL,
    [FDPID]                   INT NOT NULL,
    [Beneficiaries]           INT NOT NULL,
    CONSTRAINT [PK_RegionalRequestDetail] PRIMARY KEY CLUSTERED ([RegionalRequestDetailID] ASC),
    CONSTRAINT [FK_RegionalRequestDetail_FDP] FOREIGN KEY ([FDPID]) REFERENCES [dbo].[FDP] ([FDPID]),
    CONSTRAINT [FK_RegionalRequestDetail_RegionalRequest] FOREIGN KEY ([RegionalRequestID]) REFERENCES [EarlyWarning].[RegionalRequest] ([RegionalRequestID])
);







