CREATE TABLE [dbo].[DispatchAllocation] (
    [DispatchAllocationID]  UNIQUEIDENTIFIER CONSTRAINT [DF_DispatchAllocation_DispatchAllocationID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [PartitionID]           INT              CONSTRAINT [DF_DispatchAllocation_PartitionID] DEFAULT ((0)) NOT NULL,
    [HubID]                 INT              NOT NULL,
    [StoreID]               INT              NULL,
    [Year]                  INT              NULL,
    [Month]                 INT              NULL,
    [Round]                 INT              NULL,
    [DonorID]               INT              NULL,
    [ProgramID]             INT              NULL,
    [CommodityID]           INT              NOT NULL,
    [RequisitionNo]         NVARCHAR (50)    NOT NULL,
    [BidRefNo]              NVARCHAR (50)    NULL,
    [ContractStartDate]     DATE             NULL,
    [ContractEndDate]       DATE             NULL,
    [Beneficiery]           INT              NULL,
    [Amount]                DECIMAL (18, 3)  NOT NULL,
    [Unit]                  INT              NOT NULL,
    [TransporterID]         INT              NULL,
    [FDPID]                 INT              NOT NULL,
    [ShippingInstructionID] INT              NULL,
    [ProjectCodeID]         INT              NULL,
    [IsClosed]              BIT              CONSTRAINT [DF_DispatchAllocation_IsClosed] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DispatchAllocation] PRIMARY KEY CLUSTERED ([DispatchAllocationID] ASC),
    CONSTRAINT [FK_Allocation_FDP] FOREIGN KEY ([FDPID]) REFERENCES [dbo].[FDP] ([FDPID]),
    CONSTRAINT [FK_Allocation_Product] FOREIGN KEY ([CommodityID]) REFERENCES [dbo].[Commodity] ([CommodityID]),
    CONSTRAINT [FK_Allocation_Program] FOREIGN KEY ([ProgramID]) REFERENCES [dbo].[Program] ([ProgramID]),
    CONSTRAINT [FK_DispatchAllocation_Hub] FOREIGN KEY ([HubID]) REFERENCES [dbo].[Hub] ([HubID]),
    CONSTRAINT [FK_DispatchAllocation_ProjectCode] FOREIGN KEY ([ProjectCodeID]) REFERENCES [dbo].[ProjectCode] ([ProjectCodeID]),
    CONSTRAINT [FK_DispatchAllocation_ShippingInstruction] FOREIGN KEY ([ShippingInstructionID]) REFERENCES [dbo].[ShippingInstruction] ([ShippingInstructionID]),
    CONSTRAINT [FK_DispatchAllocation_Transporter] FOREIGN KEY ([TransporterID]) REFERENCES [dbo].[Transporter] ([TransporterID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dispatch Allocation is allocation for dispatch that is to happen for FDPs.

This table is primarily populated by data that is pushed from the transportation and bidding system. On occassions, the dispatch allocation might not go through the transportation bidding system. in such cases, this table is populated from the User interface.

One scenario that this occures is when the transportation is going to happen using DRMFSS vehicles and not by transportation company who has won the bid.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Partition at which this data has been entered/is most likely to be used.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'PartitionID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The hub from which this dispatch is to happen from.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'HubID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Year for which this dispatch is going to happen for. this comes from the Relief Requisition documents', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Month for which this dispatch is allocated for.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'For a particular Year and month, the Round for which this dispatch is to happen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'Round';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The donor of the commodity from which this dispatatch is going to happen from', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'DonorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The program from which this dispatch is going to happen from (Relief, PSNP etc)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'ProgramID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The commodity that has to be dispatched', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'CommodityID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relief Requisition Document''s Reference number (Usually 5 digit number)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'RequisitionNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'If this is an entry from the Bidding System, the reference number from the bidding system.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'BidRefNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of beneficieries that this allocation is made for', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'Beneficiery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The amount of commodity that has to be dispatched for the FDP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'Amount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unit of measure the amount reflects. by default this is in Quintals.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'Unit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The transporter that is supposed to transport this dispatch to the FDP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'TransporterID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Final Distribution Point ID.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'FDPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Describes if this allocaiton has already been closed out manually by the operator.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DispatchAllocation', @level2type = N'COLUMN', @level2name = N'IsClosed';

