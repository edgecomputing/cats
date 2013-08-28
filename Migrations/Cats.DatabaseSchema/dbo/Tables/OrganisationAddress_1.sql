CREATE TABLE [dbo].[OrganisationAddress] (
    [OrganisationAddressID] INT            IDENTITY (1, 1) NOT NULL,
    [OrganisationID]        INT            NULL,
    [Tele1]                 INT            NULL,
    [Tele2]                 INT            NULL,
    [Pobox]                 INT            NULL,
    [Email]                 NVARCHAR (100) NULL,
    [URL]                   NVARCHAR (100) NULL,
    CONSTRAINT [PK_OrganizationAddress] PRIMARY KEY CLUSTERED ([OrganisationAddressID] ASC),
    CONSTRAINT [FK_OrganisationAddress_Organisation] FOREIGN KEY ([OrganisationID]) REFERENCES [dbo].[Organisation] ([OrganisationID])
);

