CREATE TABLE [Logistics].[Ration] (
    [RationID]    INT          IDENTITY (1, 1) NOT NULL,
    [CommodityID] INT          NOT NULL,
    [Amount]      DECIMAL (18) NOT NULL,
    CONSTRAINT [PK_Ration] PRIMARY KEY CLUSTERED ([RationID] ASC)
);

