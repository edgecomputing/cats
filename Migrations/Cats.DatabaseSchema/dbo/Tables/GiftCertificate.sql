CREATE TABLE [dbo].[GiftCertificate] (
    [GiftCertificateID]  INT              IDENTITY (1, 1) NOT NULL,
    [GiftDate]           DATETIME         NOT NULL,
    [DonorID]            INT              NOT NULL,
    [SINumber]           NVARCHAR (50)    NOT NULL,
    [ReferenceNo]        NVARCHAR (50)    NOT NULL,
    [Vessel]             NVARCHAR (50)    NULL,
    [ETA]                DATE             NOT NULL,
    [IsPrinted]          BIT              NOT NULL,
    [ProgramID]          INT              NOT NULL,
    [DModeOfTransport]   INT              NOT NULL,
    [PortName]           NVARCHAR (50)    NULL,
    [DeclarationNumber]  NVARCHAR (100)   NOT NULL,
    [StatusID]           INT              NOT NULL,
    [TransactionGroupID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_GiftCertificate] PRIMARY KEY CLUSTERED ([GiftCertificateID] ASC),
    CONSTRAINT [FK_GiftCertificate_Detail] FOREIGN KEY ([DModeOfTransport]) REFERENCES [dbo].[Detail] ([DetailID]),
    CONSTRAINT [FK_GiftCertificate_Donor] FOREIGN KEY ([DonorID]) REFERENCES [dbo].[Donor] ([DonorID]),
    CONSTRAINT [FK_GiftCertificate_Program] FOREIGN KEY ([ProgramID]) REFERENCES [dbo].[Program] ([ProgramID])
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Records a gift certificate that has been submitted to the DRMFSS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary Key field for a gift certificate entry.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate', @level2type = N'COLUMN', @level2name = N'GiftCertificateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date at which the gift certificate was issued to the DRMFSS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate', @level2type = N'COLUMN', @level2name = N'GiftDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The donor for the gift certificate', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate', @level2type = N'COLUMN', @level2name = N'DonorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The shipping instruction number / batch number that identifies the given shippment', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate', @level2type = N'COLUMN', @level2name = N'SINumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The reference number that is written on the gift certificate.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate', @level2type = N'COLUMN', @level2name = N'ReferenceNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Vessile, if the shippment is coming using a vessel.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate', @level2type = N'COLUMN', @level2name = N'Vessel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estimated time of arrival for the commodities at the prot specified.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate', @level2type = N'COLUMN', @level2name = N'ETA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A boolean flag that tells if the gift certificate letter template has already been printed or not using the Gift Certificate template', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate', @level2type = N'COLUMN', @level2name = N'IsPrinted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Program for which the Gift certificate has been issued for.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate', @level2type = N'COLUMN', @level2name = N'ProgramID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A look up that tells if teh transaportation mode has been air, land or sea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate', @level2type = N'COLUMN', @level2name = N'DModeOfTransport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The name of port  at which the donation commodity is being delivered at.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificate', @level2type = N'COLUMN', @level2name = N'PortName';

