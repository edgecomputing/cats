CREATE TABLE [Logistics].[TransportRequisition] (
    [TransportRequisitionID] INT             IDENTITY (1, 1) NOT NULL,
    [TransportRequistionNo]  NVARCHAR (50)   NOT NULL,
    [RequisitionID]          INT             NOT NULL,
    [CommodityID]            INT             NOT NULL,
    [Amount]                 DECIMAL (19, 5) NOT NULL,
    [RegionID]               INT             NOT NULL,
    [ZoneID]                 INT             NOT NULL,
    [HubID]                  INT             NOT NULL,
    [SINumber]               NVARCHAR (50)   NULL,
    [ProjectCode]            INT             NULL,
    [Status]                 INT             NULL,
    [HubAllocatedBy]         INT             NULL,
    [HubAllocationDate]      DATETIME        NULL,
    [SIAllocatedBy]          INT             NULL,
    [SIAllocationDate]       DATETIME        NULL,
    [Remark]                 NVARCHAR (255)  NULL,
    CONSTRAINT [PK_TransportRequisition] PRIMARY KEY CLUSTERED ([TransportRequisitionID] ASC),
    CONSTRAINT [FK_TransportRequisition_AdminUnit] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_TransportRequisition_AdminUnit1] FOREIGN KEY ([ZoneID]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_TransportRequisition_Hub] FOREIGN KEY ([HubID]) REFERENCES [dbo].[Hub] ([HubID]),
    CONSTRAINT [FK_TransportRequisition_ProjectCode] FOREIGN KEY ([ProjectCode]) REFERENCES [dbo].[ProjectCode] ([ProjectCodeID]),
    CONSTRAINT [FK_TransportRequisition_UserProfile] FOREIGN KEY ([HubAllocatedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID]),
    CONSTRAINT [FK_TransportRequisition_UserProfile1] FOREIGN KEY ([SIAllocatedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);







