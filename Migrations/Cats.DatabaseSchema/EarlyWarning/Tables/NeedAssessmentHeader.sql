CREATE TABLE [EarlyWarning].[NeedAssessmentHeader] (
    [NAHeaderId] INT           IDENTITY (1, 1) NOT NULL,
    [NeedAID]    INT           NULL,
    [Zone]       INT           NULL,
    [Remark]     NVARCHAR (50) NULL,
    CONSTRAINT [PK_NeedAssessmentHeader] PRIMARY KEY CLUSTERED ([NAHeaderId] ASC),
    CONSTRAINT [FK_NeedAssessmentHeader_AdminUnit] FOREIGN KEY ([Zone]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_NeedAssessmentHeader_NeedAssessment] FOREIGN KEY ([NeedAID]) REFERENCES [EarlyWarning].[NeedAssessment] ([NeedAID])
);





