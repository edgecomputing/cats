CREATE TABLE [dbo].[Unit] (
    [UnitID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED ([UnitID] ASC)
);

