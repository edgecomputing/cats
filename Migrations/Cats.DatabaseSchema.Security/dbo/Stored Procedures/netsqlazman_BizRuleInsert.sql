CREATE PROCEDURE [dbo].[netsqlazman_BizRuleInsert]
(
	@BizRuleSource text,
	@BizRuleLanguage tinyint,
	@CompiledAssembly image
)
AS
INSERT INTO [dbo].[netsqlazman_BizRulesTable] ([BizRuleSource], [BizRuleLanguage], [CompiledAssembly]) VALUES (@BizRuleSource, @BizRuleLanguage, @CompiledAssembly);
RETURN SCOPE_IDENTITY()

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[netsqlazman_BizRuleInsert] TO [NetSqlAzMan_Managers]
    AS [dbo];

