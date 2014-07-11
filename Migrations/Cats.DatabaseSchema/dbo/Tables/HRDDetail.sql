CREATE TABLE [dbo].[HRDDetail] (
    [HRDDetailID]   INT IDENTITY (1, 1) NOT NULL,
    [HRDID]         INT NOT NULL,
    [Duration]      INT NOT NULL,
    [AdminUnitID]   INT NOT NULL,
    [Beneficiaries] INT NOT NULL,
    [StartingMonth] INT NOT NULL,
    CONSTRAINT [PK_HumanitarianRequirementDetail] PRIMARY KEY CLUSTERED ([HRDDetailID] ASC),
    CONSTRAINT [FK_HRDDetail_AdminUnit] FOREIGN KEY ([AdminUnitID]) REFERENCES [dbo].[AdminUnit] ([AdminUnitID]),
    CONSTRAINT [FK_HRDDetail_HRD] FOREIGN KEY ([HRDID]) REFERENCES [dbo].[HRD] ([HRDID])
);

