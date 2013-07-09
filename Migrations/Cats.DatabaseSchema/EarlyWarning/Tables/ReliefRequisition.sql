CREATE TABLE [EarlyWarning].[ReliefRequisition] (
    [RequisitionID] INT            IDENTITY (1, 1) NOT NULL,
    [CommodityID]   INT            NULL,
    [RegionID]      INT            NULL,
    [ZoneID]        INT            NULL,
    [Round]         INT            NULL,
    [RequisitionNo] NVARCHAR (255) NULL,
    [RequestedBy]   INT            NULL,
    [RequestedDate] DATETIME       NULL,
    [ApprovedBy]    INT            NULL,
    [ApprovedDate]  DATETIME       NULL,
    [Status]        INT            NULL,
    [ProgramID]     INT            NULL,
    CONSTRAINT [PK_ReliefRequisition] PRIMARY KEY CLUSTERED ([RequisitionID] ASC),
    CONSTRAINT [FK_ReliefRequisition_AdminUnit] FOREIGN KEY ([ZoneID]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_ReliefRequisition_AdminUnit1] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_ReliefRequisition_Program] FOREIGN KEY ([ProgramID]) REFERENCES [dbo].[Program] ([ProgramID]),
    CONSTRAINT [FK_ReliefRequisition_UserProfile] FOREIGN KEY ([RequestedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID]),
    CONSTRAINT [FK_ReliefRequisition_UserProfile1] FOREIGN KEY ([ApprovedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);

