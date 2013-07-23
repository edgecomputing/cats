CREATE TABLE [dbo].[netsqlazman_StoreGroupMembersTable] (
    [StoreGroupMemberId] INT            IDENTITY (1, 1) NOT NULL,
    [StoreGroupId]       INT            NOT NULL,
    [objectSid]          VARBINARY (85) NOT NULL,
    [WhereDefined]       TINYINT        NOT NULL,
    [IsMember]           BIT            NOT NULL,
    CONSTRAINT [PK_StoreGroupMembers] PRIMARY KEY CLUSTERED ([StoreGroupMemberId] ASC),
    CONSTRAINT [CK_WhereDefinedCheck] CHECK ([WhereDefined]=(0) OR [WhereDefined]>=(2) AND [WhereDefined]<=(4)),
    CONSTRAINT [FK_StoreGroupMembers_StoreGroup] FOREIGN KEY ([StoreGroupId]) REFERENCES [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_StoreGroupMembers]
    ON [dbo].[netsqlazman_StoreGroupMembersTable]([StoreGroupId] ASC, [objectSid] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [StoreGroupMembers_StoreGroupId_ObjectSid_IsMember_Unique_Index]
    ON [dbo].[netsqlazman_StoreGroupMembersTable]([StoreGroupId] ASC, [objectSid] ASC, [IsMember] ASC);

