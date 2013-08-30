CREATE TABLE [dbo].[Organisation] (
    [OrganisationID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (100) NULL,
    [Acronym]        NVARCHAR (50)  NULL,
    [IsNGO]          BIT            NULL,
    CONSTRAINT [PK_Organisation] PRIMARY KEY CLUSTERED ([OrganisationID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0-Governmental or 1- Non-Governmental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Organisation', @level2type = N'COLUMN', @level2name = N'IsNGO';

