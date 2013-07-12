CREATE TABLE [EarlyWarning].[RegionalRequest] (
    [RegionalRequestID] INT            IDENTITY (1, 1) NOT NULL,
    [RequestDate]       DATETIME       NOT NULL,
    [ProgramID]         INT            NOT NULL,
    [Round]             INT            NOT NULL,
    [RegionID]          INT            NOT NULL,
    [RequestNumber]     NVARCHAR (255) NOT NULL,
    [Year]              INT            NOT NULL,
    [Remark]            NVARCHAR (255) NULL,
    [Status]            INT            NULL,
    CONSTRAINT [PK_RegionalRequest] PRIMARY KEY CLUSTERED ([RegionalRequestID] ASC)
);







