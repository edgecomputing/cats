CREATE TABLE [dbo].[CommodityType] (
    [CommodityTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CommodityType] PRIMARY KEY CLUSTERED ([CommodityTypeID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of commodity, Food, non food for the time being. this might change in the future.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CommodityType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key field', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CommodityType', @level2type = N'COLUMN', @level2name = N'CommodityTypeID';

