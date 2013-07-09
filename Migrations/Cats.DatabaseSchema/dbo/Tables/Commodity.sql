CREATE TABLE [dbo].[Commodity] (
    [CommodityID]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (50)  NOT NULL,
    [LongName]        NVARCHAR (500) NULL,
    [NameAM]          NVARCHAR (50)  NULL,
    [CommodityCode]   NVARCHAR (3)   NULL,
    [CommodityTypeID] INT            NOT NULL,
    [ParentID]        INT            NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([CommodityID] ASC),
    CONSTRAINT [FK_Commodity_Commodity] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_Product_CommodityType] FOREIGN KEY ([CommodityTypeID]) REFERENCES [dbo].[CommodityType] ([CommodityTypeID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contains all the lists of commodities that are tracked by the system. The structure of the data in this table is a tree with only depth of 1 node. This is a table that has to be maintained centrally in the case of partitioned deployment', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Commodity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key field for Commodity', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Commodity', @level2type = N'COLUMN', @level2name = N'CommodityID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the commodity', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Commodity', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'When the name of the commodity is an abbreviation, this field contains the longer version of the commodity.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Commodity', @level2type = N'COLUMN', @level2name = N'LongName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Commodity Type ID. DRMFSS currently wants to track food and non food commodities. this field could further be re-defined as the application is being used from time to time.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Commodity', @level2type = N'COLUMN', @level2name = N'CommodityTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Commodity table tracks commodities in two levels. This field is the self reference field.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Commodity', @level2type = N'COLUMN', @level2name = N'ParentID';

