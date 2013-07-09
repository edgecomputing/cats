CREATE TABLE [EarlyWarning].[ReliefRequisitionDetail] (
    [RequisitionDetailID] INT             IDENTITY (1, 1) NOT NULL,
    [RequisitionID]       INT             NOT NULL,
    [CommodityID]         INT             NOT NULL,
    [BenficiaryNo]        INT             NOT NULL,
    [Amount]              DECIMAL (19, 5) NOT NULL,
    [FDPID]               INT             NOT NULL,
    [DonorID]             INT             NULL,
    CONSTRAINT [PK_ReliefRequisitionDetail] PRIMARY KEY CLUSTERED ([RequisitionDetailID] ASC),
    CONSTRAINT [FK_ReliefRequisitionDetail_Commodity] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_ReliefRequisitionDetail_Donor] FOREIGN KEY ([DonorID]) REFERENCES [dbo].[Donor] ([DonorID]),
    CONSTRAINT [FK_ReliefRequisitionDetail_FDP] FOREIGN KEY ([FDPID]) REFERENCES [dbo].[FDP] ([FDPID]),
    CONSTRAINT [FK_ReliefRequisitionDetail_ReliefRequisition] FOREIGN KEY ([RequisitionID]) REFERENCES [EarlyWarning].[ReliefRequisition] ([RequisitionID])
);

