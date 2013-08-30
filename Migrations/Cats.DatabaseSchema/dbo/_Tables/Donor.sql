CREATE TABLE [dbo].[Donor] (
    [DonorID]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (50)  NOT NULL,
    [DonorCode]          NVARCHAR (3)   NULL,
    [IsResponsibleDonor] BIT            NOT NULL,
    [IsSourceDonor]      BIT            NOT NULL,
    [LongName]           NVARCHAR (500) NULL,
    CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED ([DonorID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contains list of donors. There are 2 different types of donors. Responsible Donors and Source Donors. Source donors are donors that do give the actual donation. Responsible donors are those donors that bring in the commodity. WFP is for the most part responsible donor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Donor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key of Donor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Donor', @level2type = N'COLUMN', @level2name = N'DonorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of donor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Donor', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag that shows if the donor is responsible or not.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Donor', @level2type = N'COLUMN', @level2name = N'IsResponsibleDonor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag that describes if the donor is source donor or not.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Donor', @level2type = N'COLUMN', @level2name = N'IsSourceDonor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'If the donor name is an abbreviation, this field contains the longer version of the donor name', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Donor', @level2type = N'COLUMN', @level2name = N'LongName';

