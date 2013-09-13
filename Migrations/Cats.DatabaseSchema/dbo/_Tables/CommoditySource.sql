CREATE TABLE [dbo].[CommoditySource] (
    [CommoditySourceID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CommoditySource] PRIMARY KEY CLUSTERED ([CommoditySourceID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Source of Commodities for which receipts are made', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CommoditySource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Commodity Receipt Sources, Primary key, ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CommoditySource', @level2type = N'COLUMN', @level2name = N'CommoditySourceID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of commodity source', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CommoditySource', @level2type = N'COLUMN', @level2name = N'Name';

