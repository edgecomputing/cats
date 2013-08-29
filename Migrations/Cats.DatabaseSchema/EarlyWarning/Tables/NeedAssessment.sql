CREATE TABLE [EarlyWarning].[NeedAssessment] (
    [NeedAID]              INT            IDENTITY (1, 1) NOT NULL,
    [Region]               INT            NOT NULL,
    [Season]               NVARCHAR (50)  NOT NULL,
    [NeedADate]            DATETIME2 (7)  NULL,
    [NeddACreatedBy]       INT            NULL,
    [NeedAApproved]        BIT            NULL,
    [NeedAApprovedBy]      INT            NULL,
    [TypeOfNeedAssessment] NVARCHAR (50)  NULL,
    [Remark]               NVARCHAR (500) NULL,
    CONSTRAINT [PK_NeedAssessment] PRIMARY KEY CLUSTERED ([NeedAID] ASC),
    CONSTRAINT [FK_NeedAssessment_AdminUnit] FOREIGN KEY ([Region]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_NeedAssessment_UserProfile] FOREIGN KEY ([NeddACreatedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID]),
    CONSTRAINT [FK_NeedAssessment_UserProfile1] FOREIGN KEY ([NeedAApprovedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID]),
    CONSTRAINT [IX_NeedAssessment] UNIQUE NONCLUSTERED ([Region] ASC, [Season] ASC)
);








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Is need assessment from Region or Multi-agency teams from wereda', @level0type = N'SCHEMA', @level0name = N'EarlyWarning', @level1type = N'TABLE', @level1name = N'NeedAssessment', @level2type = N'COLUMN', @level2name = N'TypeOfNeedAssessment';

