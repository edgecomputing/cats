CREATE TABLE [dbo].[Contribution] (
    [ContributionID] INT IDENTITY (1, 1) NOT NULL,
    [DonorID]        INT NOT NULL,
    [HRDID]          INT NOT NULL,
    [Year]           INT NOT NULL,
    CONSTRAINT [PK_Contribution] PRIMARY KEY CLUSTERED ([ContributionID] ASC),
    CONSTRAINT [FK_Contribution_Donor] FOREIGN KEY ([DonorID]) REFERENCES [dbo].[Donor] ([DonorID]),
    CONSTRAINT [FK_Contribution_HRD] FOREIGN KEY ([HRDID]) REFERENCES [dbo].[HRD] ([HRDID])
);



