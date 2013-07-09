CREATE TABLE [dbo].[Receive] (
    [ReceiveID]                UNIQUEIDENTIFIER CONSTRAINT [DF_Receive_ReceiveID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [PartitionID]              INT              NOT NULL,
    [HubID]                    INT              NOT NULL,
    [GRN]                      NVARCHAR (7)     NOT NULL,
    [CommodityTypeID]          INT              NOT NULL,
    [SourceDonorID]            INT              NULL,
    [ResponsibleDonorID]       INT              NULL,
    [TransporterID]            INT              NOT NULL,
    [PlateNo_Prime]            NVARCHAR (50)    NULL,
    [PlateNo_Trailer]          NVARCHAR (50)    NULL,
    [DriverName]               NVARCHAR (50)    NULL,
    [WeightBridgeTicketNumber] NVARCHAR (10)    NULL,
    [WeightBeforeUnloading]    DECIMAL (18, 3)  NULL,
    [WeightAfterUnloading]     DECIMAL (18, 3)  NULL,
    [ReceiptDate]              DATETIME         NOT NULL,
    [UserProfileID]            INT              NOT NULL,
    [CreatedDate]              DATETIME         CONSTRAINT [DF_Receive_FirstRecordDate] DEFAULT (getdate()) NOT NULL,
    [WayBillNo]                NVARCHAR (50)    NULL,
    [CommoditySourceID]        INT              NOT NULL,
    [Remark]                   NVARCHAR (4000)  NULL,
    [VesselName]               NVARCHAR (50)    NULL,
    [ReceivedByStoreMan]       NVARCHAR (50)    NOT NULL,
    [PortName]                 NVARCHAR (50)    NULL,
    [PurchaseOrder]            NVARCHAR (50)    NULL,
    [SupplierName]             NVARCHAR (50)    NULL,
    [ReceiptAllocationID]      UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Receive_1] PRIMARY KEY CLUSTERED ([ReceiveID] ASC),
    CONSTRAINT [FK_Receive_CommoditySource] FOREIGN KEY ([CommoditySourceID]) REFERENCES [dbo].[CommoditySource] ([CommoditySourceID]),
    CONSTRAINT [FK_Receive_CommodityType] FOREIGN KEY ([CommodityTypeID]) REFERENCES [dbo].[CommodityType] ([CommodityTypeID]),
    CONSTRAINT [FK_Receive_Donor] FOREIGN KEY ([SourceDonorID]) REFERENCES [dbo].[Donor] ([DonorID]),
    CONSTRAINT [FK_Receive_Donor1] FOREIGN KEY ([ResponsibleDonorID]) REFERENCES [dbo].[Donor] ([DonorID]),
    CONSTRAINT [FK_Receive_Hub] FOREIGN KEY ([HubID]) REFERENCES [dbo].[Hub] ([HubID]),
    CONSTRAINT [FK_Receive_ReceiptAllocation1] FOREIGN KEY ([ReceiptAllocationID]) REFERENCES [dbo].[ReceiptAllocation] ([ReceiptAllocationID]),
    CONSTRAINT [FK_Receive_Transporter] FOREIGN KEY ([TransporterID]) REFERENCES [dbo].[Transporter] ([TransporterID]),
    CONSTRAINT [FK_Receive_UserProfile] FOREIGN KEY ([UserProfileID]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Receive]
    ON [dbo].[Receive]([GRN] ASC);

