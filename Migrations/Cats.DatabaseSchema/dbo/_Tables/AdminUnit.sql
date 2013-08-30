CREATE TABLE [dbo].[AdminUnit] (
    [AdminUnitID]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (50) NULL,
    [NameAM]          NVARCHAR (50) NULL,
    [AdminUnitTypeID] INT           NULL,
    [ParentID]        INT           NULL,
    CONSTRAINT [PK_AdminUnit] PRIMARY KEY CLUSTERED ([AdminUnitID] ASC),
    CONSTRAINT [FK_AdminUnit_AdminUnit] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_AdminUnit_AdminUnitType] FOREIGN KEY ([AdminUnitTypeID]) REFERENCES [dbo].[AdminUnitType] ([AdminUnitTypeID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Admin Units represent the structure of the governemental administration. What would be states, and counties in other parts of the world are in the Ethiopian context Regions, Zones and Woreda''s.

Notably, The Ethiopian Admnistrative structure has challenges of standardization. This table could later be enhanced to support the non standard strutures. However, A concious decission was made on the side of the development team *Including WFP counter parts, to continue with a three level admin Unit structure.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AdminUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary Key feild for Admin Units', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AdminUnit', @level2type = N'COLUMN', @level2name = N'AdminUnitID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the Admin Unit, No Type information is concatinated here.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AdminUnit', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Administrative Unit type ,', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AdminUnit', @level2type = N'COLUMN', @level2name = N'AdminUnitTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Parent Administrative Unit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AdminUnit', @level2type = N'COLUMN', @level2name = N'ParentID';

