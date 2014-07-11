CREATE TABLE [Procurement].[TransportOrderDetail] (
    [TransportOrderDetailID] INT          IDENTITY (1, 1) NOT NULL,
    [TransportOrderID]       INT          NOT NULL,
    [FdpID]                  INT          NOT NULL,
    [SourceWarehouseID]      INT          NOT NULL,
    [QuantityQtl]            DECIMAL (18) NOT NULL,
    [DistanceFromOrigin]     DECIMAL (18) NULL,
    [TariffPerQtl]           DECIMAL (18) NOT NULL,
    [RequisitionID]          INT          NOT NULL,
    [CommodityID]            INT          NOT NULL,
    [ZoneID]                 INT          NULL,
    [DonorID]                INT          NULL,
    CONSTRAINT [PK_TransportOrderDeatil] PRIMARY KEY CLUSTERED ([TransportOrderDetailID] ASC),
    CONSTRAINT [FK_TransportOrderDetail_AdminUnit] FOREIGN KEY ([ZoneID]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_TransportOrderDetail_Commodity] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_TransportOrderDetail_Donor] FOREIGN KEY ([DonorID]) REFERENCES [dbo].[Donor] ([DonorID]),
    CONSTRAINT [FK_TransportOrderDetail_FDP] FOREIGN KEY ([FdpID]) REFERENCES [dbo].[FDP] ([FDPID]),
    CONSTRAINT [FK_TransportOrderDetail_Hub] FOREIGN KEY ([SourceWarehouseID]) REFERENCES [dbo].[Hub] ([HubID]),
    CONSTRAINT [FK_TransportOrderDetail_TransportOrder] FOREIGN KEY ([TransportOrderID]) REFERENCES [Procurement].[TransportOrder] ([TransportOrderID])
);



