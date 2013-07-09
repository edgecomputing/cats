CREATE TABLE [dbo].[AdminUnitType] (
    [AdminUnitTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (50) NOT NULL,
    [NameAM]          NVARCHAR (50) NULL,
    [SortOrder]       INT           CONSTRAINT [DF_AdminUnitType_SortOrder] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AdministrativeUnitType] PRIMARY KEY CLUSTERED ([AdminUnitTypeID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Represents the different types of Administrative Units', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AdminUnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary Key field for Administrative Unit Type.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AdminUnitType', @level2type = N'COLUMN', @level2name = N'AdminUnitTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the administative unit type', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AdminUnitType', @level2type = N'COLUMN', @level2name = N'Name';

