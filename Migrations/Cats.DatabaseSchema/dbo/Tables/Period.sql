CREATE TABLE [dbo].[Period] (
    [PeriodID] INT IDENTITY (1, 1) NOT NULL,
    [Year]     INT NULL,
    [Month]    INT NULL,
    CONSTRAINT [PK_Period] PRIMARY KEY CLUSTERED ([PeriodID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'This table is a control table to list out the applicable year month combination on different lookups on the application. Mainly, this lookup is used on the dispatch allocation and dispatch screens.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Period';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Currently available years', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Period', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Currently available months.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Period', @level2type = N'COLUMN', @level2name = N'Month';

