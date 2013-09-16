CREATE TABLE [dbo].[Round] (
    [RoundID]     INT      NOT NULL,
    [RoundNumber] INT      IDENTITY (1, 1) NOT NULL,
    [StartDate]   DATETIME NULL,
    [EndDate]     DATETIME NULL,
    CONSTRAINT [PK_Round] PRIMARY KEY CLUSTERED ([RoundID] ASC)
);

