CREATE TABLE [dbo].[netsqlazman_ApplicationGroupsTable] (
    [ApplicationGroupId] INT             IDENTITY (1, 1) NOT NULL,
    [ApplicationId]      INT             NOT NULL,
    [objectSid]          VARBINARY (85)  NOT NULL,
    [Name]               NVARCHAR (255)  NOT NULL,
    [Description]        NVARCHAR (1024) NOT NULL,
    [LDapQuery]          NVARCHAR (4000) NULL,
    [GroupType]          TINYINT         NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([ApplicationGroupId] ASC),
    CONSTRAINT [CK_ApplicationGroups_GroupType_Check] CHECK ([GroupType]>=(0) AND [GroupType]<=(1)),
    CONSTRAINT [FK_ApplicationGroups_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ApplicationGroups_ApplicationId_Name_Unique_Index]
    ON [dbo].[netsqlazman_ApplicationGroupsTable]([ApplicationId] ASC, [Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationGroups]
    ON [dbo].[netsqlazman_ApplicationGroupsTable]([ApplicationId] ASC, [Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationGroups_1]
    ON [dbo].[netsqlazman_ApplicationGroupsTable]([objectSid] ASC);


GO
CREATE TRIGGER [dbo].[ApplicationGroupDeleteTrigger] ON [dbo].[netsqlazman_ApplicationGroupsTable] 
FOR DELETE 
AS
DECLARE @DELETEDOBJECTSID int
DECLARE applicationgroups_cur CURSOR FAST_FORWARD FOR SELECT objectSid FROM deleted
OPEN applicationgroups_cur
FETCH NEXT from applicationgroups_cur INTO @DELETEDOBJECTSID
WHILE @@FETCH_STATUS = 0
BEGIN
	DELETE FROM dbo.[netsqlazman_ApplicationGroupMembersTable] WHERE objectSid = @DELETEDOBJECTSID AND WhereDefined = 1
	DELETE FROM dbo.[netsqlazman_AuthorizationsTable] WHERE objectSid = @DELETEDOBJECTSID AND objectSidWhereDefined = 1
	FETCH NEXT from applicationgroups_cur INTO @DELETEDOBJECTSID
END
CLOSE applicationgroups_cur
DEALLOCATE applicationgroups_cur
