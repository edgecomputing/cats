CREATE TABLE [dbo].[netsqlazman_BizRulesTable] (
    [BizRuleId]        INT     IDENTITY (1, 1) NOT NULL,
    [BizRuleSource]    TEXT    NOT NULL,
    [BizRuleLanguage]  TINYINT NOT NULL,
    [CompiledAssembly] IMAGE   NOT NULL,
    CONSTRAINT [PK_BizRules] PRIMARY KEY CLUSTERED ([BizRuleId] ASC)
);

