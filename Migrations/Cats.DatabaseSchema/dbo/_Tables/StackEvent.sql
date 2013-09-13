CREATE TABLE [dbo].[StackEvent] (
    [StackEventID]      UNIQUEIDENTIFIER CONSTRAINT [DF_StackEvent_StackEventID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [EventDate]         DATETIME         CONSTRAINT [DF_StackEvent_EventDate] DEFAULT (getdate()) NOT NULL,
    [HubID]             INT              NOT NULL,
    [StoreID]           INT              NOT NULL,
    [StackEventTypeID]  INT              NOT NULL,
    [StackNumber]       INT              NOT NULL,
    [FollowUpDate]      DATETIME         NULL,
    [FollowUpPerformed] BIT              CONSTRAINT [DF_StackEvent_FollowUpPerformed] DEFAULT ((0)) NOT NULL,
    [Description]       NVARCHAR (4000)  NOT NULL,
    [Recommendation]    NVARCHAR (4000)  NULL,
    [UserProfileID]     INT              NOT NULL,
    CONSTRAINT [PK_StackEvent] PRIMARY KEY CLUSTERED ([StackEventID] ASC),
    CONSTRAINT [FK_StackEvent_StackEventType] FOREIGN KEY ([StackEventTypeID]) REFERENCES [dbo].[StackEventType] ([StackEventTypeID])
);

