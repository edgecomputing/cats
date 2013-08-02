CREATE TABLE [EarlyWarning].[NeedAssessmentDetail] (
    [NAId]        INT IDENTITY (1, 1) NOT NULL,
    [NAHeaderId]  INT NOT NULL,
    [VPoorNoOfM]  INT NULL,
    [VPoorNoOfB]  INT NULL,
    [PoorNoOfM]   INT NULL,
    [PoorNoOfB]   INT NULL,
    [MiddleNoOfM] INT NULL,
    [MiddleNoOfB] INT NULL,
    [BOffNoOfM]   INT NULL,
    [BOffNoOfB]   INT NULL,
    [Zone]        INT NULL,
    [District]    INT NULL,
    CONSTRAINT [PK_NeedAssement] PRIMARY KEY CLUSTERED ([NAId] ASC),
    CONSTRAINT [FK_NeedAssessmentDetail_AdminUnit] FOREIGN KEY ([Zone]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_NeedAssessmentDetail_AdminUnit1] FOREIGN KEY ([District]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_NeedAssessmentDetail_NeedAssessmentHeader] FOREIGN KEY ([NAHeaderId]) REFERENCES [EarlyWarning].[NeedAssessmentHeader] ([NAHeaderId])
);

