CREATE TABLE [dbo].[GiftCertificateDetail] (
    [GiftCertificateDetailID] INT             IDENTITY (1, 1) NOT NULL,
    [PartitionID]             INT             NOT NULL,
    [TransactionGroupID]      INT             NOT NULL,
    [GiftCertificateID]       INT             NOT NULL,
    [CommodityID]             INT             NOT NULL,
    [WeightInMT]              DECIMAL (18, 3) NOT NULL,
    [BillOfLoading]           NVARCHAR (50)   NULL,
    [AccountNumber]           INT             NOT NULL,
    [EstimatedPrice]          DECIMAL (18, 2) NOT NULL,
    [EstimatedTax]            DECIMAL (18, 2) NOT NULL,
    [YearPurchased]           INT             NOT NULL,
    [DFundSourceID]           INT             NOT NULL,
    [DCurrencyID]             INT             NOT NULL,
    [DBudgetTypeID]           INT             NOT NULL,
    [ExpiryDate]              DATETIME        NULL,
    CONSTRAINT [PK_GiftCertificateDetail_1] PRIMARY KEY CLUSTERED ([GiftCertificateDetailID] ASC),
    CONSTRAINT [FK_GiftCertificateDetail_Commodity] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_GiftCertificateDetail_Detail] FOREIGN KEY ([DFundSourceID]) REFERENCES [dbo].[Detail] ([DetailID]),
    CONSTRAINT [FK_GiftCertificateDetail_Detail1] FOREIGN KEY ([DCurrencyID]) REFERENCES [dbo].[Detail] ([DetailID]),
    CONSTRAINT [FK_GiftCertificateDetail_Detail2] FOREIGN KEY ([DBudgetTypeID]) REFERENCES [dbo].[Detail] ([DetailID]),
    CONSTRAINT [FK_GiftCertificateDetail_GiftCertificate] FOREIGN KEY ([GiftCertificateID]) REFERENCES [dbo].[GiftCertificate] ([GiftCertificateID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detail commodtiy information that is to be delivered on a given gift certificate.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gift certificate detail ID,  A primary key field with auto numbering enabled', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'GiftCertificateDetailID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Partition ID, this is an obsolate field which was inserted to make this table work with the transactions table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'PartitionID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deprecated field that used to contain reference to a transaction table. this has now been removed from the transactions table and no longer important to maintain the transaction group id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'TransactionGroupID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The reference to the specific gift certificate this detail information exists for.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'GiftCertificateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID of the commodity received in this gift certifcate detail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'CommodityID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The weight in metric tone of the commodities delviered.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'WeightInMT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The bill of loading for each commodtiy load delivered in this gift certificate', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'BillOfLoading';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Finance account number from which taxation for this import should be calculated', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'AccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The estimated price of the commodties in this list.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'EstimatedPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estimated tax that will be paid for the commodities donated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'EstimatedTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Year purchased in gregorian calendar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'YearPurchased';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Source of fund', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'DFundSourceID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Currency at which the value of the commodtiy is measured, USD, BIR, EUR etc', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'DCurrencyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of budget through which this commodity is aquired through.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'DBudgetTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contains the Expected Expiry date of the commodities on this shipment. This is not always tracked, and only applies to Food Commodities.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GiftCertificateDetail', @level2type = N'COLUMN', @level2name = N'ExpiryDate';

