CREATE TABLE [Procurement].[TransportOrderDetail] (
    [TransportOrderDetailID] INT          IDENTITY (1, 1) NOT NULL,
    [TransportOrderID]       INT          NOT NULL,
    [FdpID]                  INT          NOT NULL,
    [SourceWarehouseID]      INT          NOT NULL,
    [QuantityQtl]            DECIMAL (18) NOT NULL,
    [DistanceFromOrigin]     DECIMAL (18) NULL,
    [TariffPerQtl]           DECIMAL (18) NOT NULL,
    CONSTRAINT [PK_TransportOrderDeatil] PRIMARY KEY CLUSTERED ([TransportOrderDetailID] ASC),
    CONSTRAINT [FK_TransportOrderDetail_FDP] FOREIGN KEY ([FdpID]) REFERENCES [dbo].[FDP] ([FDPID]),
    CONSTRAINT [FK_TransportOrderDetail_Hub] FOREIGN KEY ([SourceWarehouseID]) REFERENCES [dbo].[Hub] ([HubID]),
    CONSTRAINT [FK_TransportOrderDetail_TransportOrder] FOREIGN KEY ([TransportOrderID]) REFERENCES [Procurement].[TransportOrder] ([TransportOrderID])
);

