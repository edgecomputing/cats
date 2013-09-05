CREATE TABLE [dbo].[Currency] (
    [CurrencyID] INT          IDENTITY (1, 1) NOT NULL,
    [Code]       VARCHAR (5)  NOT NULL,
    [Name]       VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([CurrencyID] ASC)
);

