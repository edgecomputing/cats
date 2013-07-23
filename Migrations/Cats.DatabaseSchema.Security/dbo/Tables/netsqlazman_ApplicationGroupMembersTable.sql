CREATE TABLE [dbo].[netsqlazman_ApplicationGroupMembersTable] (
    [ApplicationGroupMemberId] INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationGroupId]       INT            NOT NULL,
    [objectSid]                VARBINARY (85) NOT NULL,
    [WhereDefined]             TINYINT        NOT NULL,
    [IsMember]                 BIT            NOT NULL,
    CONSTRAINT [PK_GroupMembers] PRIMARY KEY CLUSTERED ([ApplicationGroupMemberId] ASC),
    CONSTRAINT [CK_WhereDefinedNotValid] CHECK ([WhereDefined]>=(0) AND [WhereDefined]<=(4)),
    CONSTRAINT [FK_ApplicationGroupMembers_ApplicationGroup] FOREIGN KEY ([ApplicationGroupId]) REFERENCES [dbo].[netsqlazman_ApplicationGroupsTable] ([ApplicationGroupId]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ApplicationGroupMembers_ApplicationGroupId_ObjectSid_IsMember_Unique_Index]
    ON [dbo].[netsqlazman_ApplicationGroupMembersTable]([ApplicationGroupId] ASC, [objectSid] ASC, [IsMember] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationGroupMembers]
    ON [dbo].[netsqlazman_ApplicationGroupMembersTable]([ApplicationGroupId] ASC, [objectSid] ASC);

