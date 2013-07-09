CREATE TABLE [dbo].[CommodityGrade] (
    [CommodityGradeID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50) NOT NULL,
    [Description]      NVARCHAR (50) NULL,
    [SortOrder]        INT           CONSTRAINT [DF_CommodityGrade_SortOrder] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CommodityGrade] PRIMARY KEY CLUSTERED ([CommodityGradeID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grade of commodities.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CommodityGrade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the commodity grade', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CommodityGrade', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Short description of the commodity grade', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CommodityGrade', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The order of the commodity grade', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CommodityGrade', @level2type = N'COLUMN', @level2name = N'SortOrder';

