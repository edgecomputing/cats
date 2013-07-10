CREATE TABLE [Procurement].[TransportOrder] (
    [TransportOrderID]         INT           IDENTITY (1, 1) NOT NULL,
    [TransportOrderNo]         NVARCHAR (50) NOT NULL,
    [OrderDate]                DATETIME      NOT NULL,
    [RequestedDispatchDate]    DATETIME      NOT NULL,
    [OrderExpiryDate]          DATETIME      NOT NULL,
    [BidDocumentNo]            NVARCHAR (50) NOT NULL,
    [PerformanceBondReceiptNo] NVARCHAR (50) NULL,
    [TransporterID]            INT           NOT NULL,
    [ConsignerName]            NVARCHAR (50) NULL,
    [TransporterSignedName]    NVARCHAR (50) NULL,
    [ConsignerDate]            DATETIME      NULL,
    [TransporterSignedDate]    DATETIME      NULL,
    [ContractNumber]           NVARCHAR (50) NULL,
    CONSTRAINT [PK_TransportOrder] PRIMARY KEY CLUSTERED ([TransportOrderID] ASC),
    CONSTRAINT [FK_TransportOrder_Transporter] FOREIGN KEY ([TransporterID]) REFERENCES [Procurement].[Transporter] ([TransporterID])
);

