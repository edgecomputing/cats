CREATE TABLE [EarlyWarning].[NeedAssessmentHeader] (
    [NAHeaderId]       INT           IDENTITY (1, 1) NOT NULL,
    [NeedACreatedDate] DATETIME      NULL,
    [NeddACreatedBy]   INT           NULL,
    [NeedAApproved]    BIT           NULL,
    [Remark]           NVARCHAR (50) NULL,
    CONSTRAINT [PK_NeedAssessmentHeader] PRIMARY KEY CLUSTERED ([NAHeaderId] ASC),
    CONSTRAINT [FK_NeedAssessmentHeader_UserProfile] FOREIGN KEY ([NeddACreatedBy]) REFERENCES [dbo].[UserProfile] ([UserProfileID])
);



