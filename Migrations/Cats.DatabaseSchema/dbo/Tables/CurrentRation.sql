CREATE TABLE [dbo].[CurrentRation] (
    [CurrentRationID] INT          NOT NULL,
    [RationID]        INT          NOT NULL,
    [CommodityID]     INT          NOT NULL,
    [Amount]          DECIMAL (18) NOT NULL,
    [ModifiedDate]    DATETIME     NULL,
    [ModifiedBy]      INT          NULL,
    CONSTRAINT [PK_CurrentRation] PRIMARY KEY CLUSTERED ([CurrentRationID] ASC)
);

