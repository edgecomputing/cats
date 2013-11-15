CREATE TABLE [EarlyWarning].[RegionalRequest] (
    [RegionalRequestID] INT            IDENTITY (1, 1) NOT NULL,
    [RequestDate]       DATETIME       NOT NULL,
    [ProgramID]         INT            NOT NULL,
    [RationID]          INT            NOT NULL,
    [Month]             INT            NOT NULL,
    [RegionID]          INT            NOT NULL,
    [RequestNumber]     NVARCHAR (255) NOT NULL,
    [Year]              INT            NOT NULL,
    [Remark]            NVARCHAR (255) NULL,
    [Status]            INT            NULL,
    CONSTRAINT [PK_RegionalRequest] PRIMARY KEY CLUSTERED ([RegionalRequestID] ASC),
    CONSTRAINT [FK_RegionalRequest_AdminUnit] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_RegionalRequest_Program] FOREIGN KEY ([ProgramID]) REFERENCES [dbo].[Program] ([ProgramID])
    CONSTRAINT [FK_RegionalRequest_Ration] FOREIGN KEY ([RationID]) REFERENCES [dbo].[Ration] ([RationID])
);











