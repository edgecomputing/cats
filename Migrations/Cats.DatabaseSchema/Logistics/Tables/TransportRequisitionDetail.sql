CREATE TABLE [Logistics].[TransportRequisitionDetail] (
    [TransportRequisitionDetailID] INT IDENTITY (1, 1) NOT NULL,
    [TransportRequisitionID]       INT NOT NULL,
    [RequisitionID]                INT NOT NULL,
    CONSTRAINT [PK_TransportRequisitionDetail] PRIMARY KEY CLUSTERED ([TransportRequisitionDetailID] ASC),
    CONSTRAINT [FK_TransportRequisitionDetail_ReliefRequisition] FOREIGN KEY ([RequisitionID]) REFERENCES [EarlyWarning].[ReliefRequisition] ([RequisitionID]),
    CONSTRAINT [FK_TransportRequisitionDetail_TransportRequisition] FOREIGN KEY ([TransportRequisitionID]) REFERENCES [Logistics].[TransportRequisition] ([TransportRequisitionID])
);

