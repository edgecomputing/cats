CREATE TABLE [dbo].[FDP] (
    [FDPID]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [NameAM]      NVARCHAR (50) NULL,
    [AdminUnitID] INT           NOT NULL,
    CONSTRAINT [PK_FDP] PRIMARY KEY CLUSTERED ([FDPID] ASC),
    CONSTRAINT [FK_FDP_AdminUnit] FOREIGN KEY ([AdminUnitID]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contains a list of Final Distribution Points. Note that in the Administrative unit structure, an FDP exists under a specific woreda. This assumption is taken or derived from the Admin Units being organized in the current way of organization which allows to manage three leves of admin unit.

If the admin unit is to represent the complex structure of Ethiopian Admin Units where there is no standard in the who reports to what, the FDP could also be pointing to non Woreda Administrative units.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FDP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key field that represents the FDP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FDP', @level2type = N'COLUMN', @level2name = N'FDPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the FDP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FDP', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The woreda ID under which the FDP exists.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FDP', @level2type = N'COLUMN', @level2name = N'AdminUnitID';

