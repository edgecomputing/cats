CREATE TABLE [dbo].[UserHub] (
    [UserHubID]     INT      IDENTITY (1, 1) NOT NULL,
    [UserProfileID] INT      NOT NULL,
    [HubID]         INT      NOT NULL,
    [IsDefault]     CHAR (1) NULL,
    CONSTRAINT [PK_UserWarehouse] PRIMARY KEY CLUSTERED ([UserHubID] ASC),
    CONSTRAINT [FK_UserWarehouse_UserProfile] FOREIGN KEY ([UserProfileID]) REFERENCES [dbo].[UserProfile] ([UserProfileID]),
    CONSTRAINT [FK_UserWarehouse_Warehouse] FOREIGN KEY ([HubID]) REFERENCES [dbo].[Hub] ([HubID])
);



