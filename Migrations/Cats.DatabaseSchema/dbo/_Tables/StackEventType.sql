CREATE TABLE [dbo].[StackEventType] (
    [StackEventTypeID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]                    NVARCHAR (50)  NOT NULL,
    [Description]             NVARCHAR (500) NULL,
    [Periodic]                BIT            NOT NULL,
    [DefaultFollowUpDuration] INT            NULL,
    CONSTRAINT [PK_StackEventType] PRIMARY KEY CLUSTERED ([StackEventTypeID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Follow up after number of days. Zero means this event type doesn''t require follow up.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'StackEventType', @level2type = N'COLUMN', @level2name = N'DefaultFollowUpDuration';

