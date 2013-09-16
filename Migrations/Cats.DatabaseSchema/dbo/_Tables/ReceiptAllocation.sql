CREATE TABLE [dbo].[ReceiptAllocation] (
    [ReceiptAllocationID]     UNIQUEIDENTIFIER CONSTRAINT [DF_ReceiptAllocation_ReceiptAllocationID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [PartitionID]             INT              NOT NULL,
    [IsCommited]              BIT              CONSTRAINT [DF_ReceiptAllocation_IsCommited] DEFAULT ((0)) NOT NULL,
    [ETA]                     DATE             NOT NULL,
    [ProjectNumber]           NVARCHAR (50)    NOT NULL,
    [GiftCertificateDetailID] INT              NULL,
    [CommodityID]             INT              NOT NULL,
    [SINumber]                NVARCHAR (50)    NULL,
    [UnitID]                  INT              NULL,
    [QuantityInUnit]          DECIMAL (18, 3)  NULL,
    [QuantityInMT]            DECIMAL (18, 3)  NOT NULL,
    [HubID]                   INT              NOT NULL,
    [DonorID]                 INT              NULL,
    [ProgramID]               INT              NOT NULL,
    [CommoditySourceID]       INT              NOT NULL,
    [IsClosed]                BIT              CONSTRAINT [DF_ReceiptAllocation_IsClosed] DEFAULT ((0)) NOT NULL,
    [PurchaseOrder]           NVARCHAR (50)    NULL,
    [SupplierName]            NVARCHAR (50)    NULL,
    [SourceHubID]             INT              NULL,
    [OtherDocumentationRef]   NVARCHAR (50)    NULL,
    [Remark]                  NVARCHAR (50)    NULL,
    CONSTRAINT [PK_ReceiptAllocation] PRIMARY KEY CLUSTERED ([ReceiptAllocationID] ASC),
    CONSTRAINT [FK_ReceiptAllocation_Commodity] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_ReceiptAllocation_CommoditySource] FOREIGN KEY ([CommoditySourceID]) REFERENCES [dbo].[CommoditySource] ([CommoditySourceID]),
    CONSTRAINT [FK_ReceiptAllocation_Donor] FOREIGN KEY ([DonorID]) REFERENCES [dbo].[Donor] ([DonorID]),
    CONSTRAINT [FK_ReceiptAllocation_GiftCertificateDetail] FOREIGN KEY ([GiftCertificateDetailID]) REFERENCES [dbo].[GiftCertificateDetail] ([GiftCertificateDetailID]),
    CONSTRAINT [FK_ReceiptAllocation_Hub] FOREIGN KEY ([HubID]) REFERENCES [dbo].[Hub] ([HubID]),
    CONSTRAINT [FK_ReceiptAllocation_Hub1] FOREIGN KEY ([SourceHubID]) REFERENCES [dbo].[Hub] ([HubID]),
    CONSTRAINT [FK_ReceiptAllocation_Program] FOREIGN KEY ([ProgramID]) REFERENCES [dbo].[Program] ([ProgramID]),
    CONSTRAINT [FK_ReceiptAllocation_Unit] FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Unit] ([UnitID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Receipt allocation is a table that tracks (stores the allocation that happens before a receipt is made at the hub. This table represnets the plan that has happened when a specific allocation is performed.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The partition at which the given transaction or receipt is to occur.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation', @level2type = N'COLUMN', @level2name = N'PartitionID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estimated time of arrival at the hubs. (this value is different from the one on the gift certificate because this one is the estimated arrival time at the hub rather than the port)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation', @level2type = N'COLUMN', @level2name = N'ETA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The newly assigned project code, this is a copy of the expected project code. the reason for this is that until the first batch of commodties arrive at the hub, we are not sure if this project code should be created in the project code table or not.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation', @level2type = N'COLUMN', @level2name = N'ProjectNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'If this current allocation is from a gift certificate, this field contains a reference from the gift certificate detail table,', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation', @level2type = N'COLUMN', @level2name = N'GiftCertificateDetailID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A reference to the commoditiy referenced in the commodity receipt allocaiton.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation', @level2type = N'COLUMN', @level2name = N'CommodityID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The shipping instruction number.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation', @level2type = N'COLUMN', @level2name = N'SINumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quantity in Mettric tonne that is allocated to the defaut / current warehouse.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation', @level2type = N'COLUMN', @level2name = N'QuantityInMT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hub ID, a reference to the hub to which this current allocation is to be sent to.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation', @level2type = N'COLUMN', @level2name = N'HubID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The donor from which the commodity allocated is coming from.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation', @level2type = N'COLUMN', @level2name = N'DonorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The DRMFSS program for which the current allocation is performed for', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation', @level2type = N'COLUMN', @level2name = N'ProgramID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The source of commodity from which this commodity is allocated from (Donation, Local purchase, Exchange, etc)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReceiptAllocation', @level2type = N'COLUMN', @level2name = N'CommoditySourceID';

