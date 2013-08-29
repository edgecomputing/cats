CREATE TABLE [dbo].[Ration] (
    [RationID]        INT           IDENTITY (1, 1) NOT NULL,
    [CreatedDate]     DATETIME      NULL,
    [CreatedBy]       INT           NULL,
    [UpdatedDate]     DATETIME      NULL,
    [UpdatedBy]       INT           NULL,
    [IsDefaultRation] BIT           CONSTRAINT [DF_Ration_IsDefaultRation] DEFAULT ((0)) NOT NULL,
    [RefrenceNumber]  NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Ration_1] PRIMARY KEY CLUSTERED ([RationID] ASC)
);



