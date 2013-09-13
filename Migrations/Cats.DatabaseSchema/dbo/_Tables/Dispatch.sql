CREATE TABLE [dbo].[Dispatch] (
    [DispatchID]                UNIQUEIDENTIFIER CONSTRAINT [DF_Dispatch_DispatchID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [PartitionID]               INT              NOT NULL,
    [HubID]                     INT              NOT NULL,
    [GIN]                       NVARCHAR (7)     NOT NULL,
    [FDPID]                     INT              NULL,
    [WeighBridgeTicketNumber]   NVARCHAR (50)    NULL,
    [RequisitionNo]             NVARCHAR (50)    NOT NULL,
    [BidNumber]                 NVARCHAR (50)    NOT NULL,
    [TransporterID]             INT              NOT NULL,
    [DriverName]                NVARCHAR (50)    NOT NULL,
    [PlateNo_Prime]             NVARCHAR (50)    NOT NULL,
    [PlateNo_Trailer]           NVARCHAR (50)    NULL,
    [PeriodYear]                INT              NOT NULL,
    [PeriodMonth]               INT              NOT NULL,
    [Round]                     INT              NOT NULL,
    [UserProfileID]             INT              NOT NULL,
    [DispatchDate]              DATETIME         NOT NULL,
    [CreatedDate]               DATETIME         CONSTRAINT [DF_Dispatch_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [Remark]                    NVARCHAR (4000)  NULL,
    [DispatchedByStoreMan]      NVARCHAR (50)    NOT NULL,
    [DispatchAllocationID]      UNIQUEIDENTIFIER NULL,
    [OtherDispatchAllocationID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Dispatch] PRIMARY KEY CLUSTERED ([DispatchID] ASC),
    CONSTRAINT [FK_Dispatch_DispatchAllocation] FOREIGN KEY ([DispatchAllocationID]) REFERENCES [dbo].[DispatchAllocation] ([DispatchAllocationID]),
    CONSTRAINT [FK_Dispatch_FDP] FOREIGN KEY ([FDPID]) REFERENCES [dbo].[FDP] ([FDPID]),
    CONSTRAINT [FK_Dispatch_Hub] FOREIGN KEY ([HubID]) REFERENCES [dbo].[Hub] ([HubID]),
    CONSTRAINT [FK_Dispatch_OtherDispatchAllocation] FOREIGN KEY ([OtherDispatchAllocationID]) REFERENCES [dbo].[OtherDispatchAllocation] ([OtherDispatchAllocationID]),
    CONSTRAINT [FK_Dispatch_Transporter] FOREIGN KEY ([TransporterID]) REFERENCES [dbo].[Transporter] ([TransporterID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Dispatch]
    ON [dbo].[Dispatch]([GIN] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [GIN]
    ON [dbo].[Dispatch]([GIN] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Records all fields that have to deal with the physical movement of the stock disptached.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Partition number, this is a number that is assigned to the installation instance number. On later phases this number could be something different from the HubID.  this is kept as a separate field.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'PartitionID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The hub this specific dispatch transaction happened in.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'HubID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A Unique 6 digit number that represents the Goods Issue Note. This is a pre-printed number on the paper Invoices.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'GIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Final Distribution Point ID, to which this specific dispatch happened for. If this is null, it means that the dispatch is for something diffenent than FDP. This other entity could be other Hub Owners like WFP, EFSRA etc', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'FDPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'If the vehicle was weighed after loading the commodities, this field should track the weight bridg ticket number', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'WeighBridgeTicketNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Requisition number from the Relief Requisition Document (RRD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'RequisitionNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bid Number, from the transportation system.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'BidNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The transporter ID from the transportation bidding system', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'TransporterID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Full name of the driver that is taking the cargo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'DriverName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prime plate number.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'PlateNo_Prime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'plate number of the trailer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'PlateNo_Trailer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The year for which the dispatch was allocated for', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'PeriodYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The month for which the dispatch was allocated for.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'PeriodMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The round for which the cargo dispatched was allocated for. A given allocation Year and Month combination could have a number of rounds.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'Round';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The user profile ID of the person that recorded the dispatch transaction', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'UserProfileID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date recorded on the dispatch User Interface', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'DispatchDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'System date of the time this record was created', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Remarks if there were any on the dispatch screen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'Remark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The name of the store man who dispatched this list of commodities.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Dispatch', @level2type = N'COLUMN', @level2name = N'DispatchedByStoreMan';

