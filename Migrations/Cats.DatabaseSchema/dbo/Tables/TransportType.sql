CREATE TABLE [dbo].[TransportType] (
    [TransportTypeID] INT           NOT NULL,
    [Name]            NVARCHAR (50) NULL,
    CONSTRAINT [PK_TransportType] PRIMARY KEY CLUSTERED ([TransportTypeID] ASC)
);

