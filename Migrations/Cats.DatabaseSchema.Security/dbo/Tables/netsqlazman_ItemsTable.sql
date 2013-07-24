CREATE TABLE [dbo].[netsqlazman_ItemsTable] (
    [ItemId]        INT             IDENTITY (1, 1) NOT NULL,
    [ApplicationId] INT             NOT NULL,
    [Name]          NVARCHAR (255)  NOT NULL,
    [Description]   NVARCHAR (1024) NOT NULL,
    [ItemType]      TINYINT         NOT NULL,
    [BizRuleId]     INT             NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED ([ItemId] ASC),
    CONSTRAINT [CK_Items_ItemTypeCheck] CHECK ([ItemType]>=(0) AND [ItemType]<=(2)),
    CONSTRAINT [FK_Items_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Items_BizRules] FOREIGN KEY ([BizRuleId]) REFERENCES [dbo].[netsqlazman_BizRulesTable] ([BizRuleId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Items_ApplicationId_Name_Unique_Index]
    ON [dbo].[netsqlazman_ItemsTable]([Name] ASC, [ApplicationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Items]
    ON [dbo].[netsqlazman_ItemsTable]([ApplicationId] ASC, [Name] ASC);


GO
CREATE TRIGGER [dbo].[ItemDeleteTrigger] ON [dbo].[netsqlazman_ItemsTable] 
FOR DELETE 
AS
DECLARE @DELETEDITEMID int
DECLARE @BIZRULEID int
DECLARE items_cur CURSOR FAST_FORWARD FOR SELECT ItemId, BizRuleId FROM deleted
OPEN items_cur
FETCH NEXT from items_cur INTO @DELETEDITEMID, @BIZRULEID
WHILE @@FETCH_STATUS = 0
BEGIN
	DELETE FROM dbo.[netsqlazman_ItemsHierarchyTable] WHERE ItemId = @DELETEDITEMID OR MemberOfItemId = @DELETEDITEMID
	IF @BIZRULEID IS NOT NULL
		DELETE FROM dbo.[netsqlazman_BizRulesTable] WHERE BizRuleId = @BIZRULEID
	FETCH NEXT from items_cur INTO @DELETEDITEMID, @BIZRULEID
END
CLOSE items_cur
DEALLOCATE items_cur
