CREATE TABLE [EarlyWarning].[NeedAssessmentDetail] (
    [NAId]                     INT            IDENTITY (1, 1) NOT NULL,
    [NeedAID]                  INT            NULL,
    [Woreda]                   INT            NULL,
    [ProjectedMale]            INT            NULL,
    [ProjectedFemale]          INT            NULL,
    [RegularPSNP]              INT            NULL,
    [PSNP]                     INT            NULL,
    [NonPSNP]                  INT            NULL,
    [Contingencybudget]        INT            NULL,
    [TotalBeneficiaries]       INT            NULL,
    [PSNPFromWoredasMale]      INT            NULL,
    [PSNPFromWoredasFemale]    INT            NULL,
    [PSNPFromWoredasDOA]       INT            NULL,
    [NonPSNPFromWoredasMale]   INT            NULL,
    [NonPSNPFromWoredasFemale] INT            NULL,
    [NonPSNPFromWoredasDOA]    INT            NULL,
    [Remark]                   NVARCHAR (500) NULL,
    CONSTRAINT [PK_NeedAssement] PRIMARY KEY CLUSTERED ([NAId] ASC),
    CONSTRAINT [FK_NeedAssessmentDetail_AdminUnit] FOREIGN KEY ([Woreda]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_NeedAssessmentDetail_NeedAssessmentHeader] FOREIGN KEY ([NeedAID]) REFERENCES [EarlyWarning].[NeedAssessmentHeader] ([NAHeaderId]) ON DELETE CASCADE
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of Regular PSNP beneficiaries. Non-graduated', @level0type = N'SCHEMA', @level0name = N'EarlyWarning', @level1type = N'TABLE', @level1name = N'NeedAssessmentDetail', @level2type = N'COLUMN', @level2name = N'RegularPSNP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of beneficiaries from PSNP Woredas(Emergency/Transitory need). Duration of Assistance', @level0type = N'SCHEMA', @level0name = N'EarlyWarning', @level1type = N'TABLE', @level1name = N'NeedAssessmentDetail', @level2type = N'COLUMN', @level2name = N'PSNPFromWoredasDOA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of needy population identified through assessment and endorsed by the Region/Administration', @level0type = N'SCHEMA', @level0name = N'EarlyWarning', @level1type = N'TABLE', @level1name = N'NeedAssessmentDetail', @level2type = N'COLUMN', @level2name = N'PSNP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Projected population', @level0type = N'SCHEMA', @level0name = N'EarlyWarning', @level1type = N'TABLE', @level1name = N'NeedAssessmentDetail', @level2type = N'COLUMN', @level2name = N'ProjectedMale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Projected population', @level0type = N'SCHEMA', @level0name = N'EarlyWarning', @level1type = N'TABLE', @level1name = N'NeedAssessmentDetail', @level2type = N'COLUMN', @level2name = N'ProjectedFemale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of beneficiaries from non-PSNP Woredas. Duration of Assistance', @level0type = N'SCHEMA', @level0name = N'EarlyWarning', @level1type = N'TABLE', @level1name = N'NeedAssessmentDetail', @level2type = N'COLUMN', @level2name = N'NonPSNPFromWoredasDOA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of needy population identified through assessment and endorsed by the Region/Administration', @level0type = N'SCHEMA', @level0name = N'EarlyWarning', @level1type = N'TABLE', @level1name = N'NeedAssessmentDetail', @level2type = N'COLUMN', @level2name = N'NonPSNP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of people to be covered through 15% Contingency budget held at the regional level', @level0type = N'SCHEMA', @level0name = N'EarlyWarning', @level1type = N'TABLE', @level1name = N'NeedAssessmentDetail', @level2type = N'COLUMN', @level2name = N'Contingencybudget';

