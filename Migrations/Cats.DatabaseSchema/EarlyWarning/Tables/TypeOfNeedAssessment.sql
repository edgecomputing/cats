CREATE TABLE [EarlyWarning].[TypeOfNeedAssessment] (
    [TypeOfNeedAssessmentID] INT           IDENTITY (1, 1) NOT NULL,
    [TypeOfNeedAssessment]   NVARCHAR (50) NOT NULL,
    [Remark]                 NCHAR (10)    NULL,
    CONSTRAINT [PK_TypeOfNeedAssessment] PRIMARY KEY CLUSTERED ([TypeOfNeedAssessmentID] ASC),
    CONSTRAINT [IX_TypeOfNeedAssessment] UNIQUE NONCLUSTERED ([TypeOfNeedAssessment] ASC)
);

