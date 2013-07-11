CREATE TABLE [Logistics].[TransportRequisition] (
    [TransportRequisitionID] INT           IDENTITY (1, 1) NOT NULL,
    [TransportRequisitionNo] NVARCHAR (50) NOT NULL,
    [RequestedBy]            INT           NOT NULL,
    [RequestedDate]          DATETIME      NOT NULL,
    [CertifiedBy]            INT           NOT NULL,
    [CertifiedDate]          DATETIME      NOT NULL,
    [Remark]                 TEXT          NULL,
    [Status]                 INT           NOT NULL,
    CONSTRAINT [PK_TransportRequisition] PRIMARY KEY CLUSTERED ([TransportRequisitionID] ASC),
    CONSTRAINT [FK_TransportRequisition_UserProfile] FOREIGN KEY ([RequestedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID]),
    CONSTRAINT [FK_TransportRequisition_UserProfile1] FOREIGN KEY ([CertifiedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);



