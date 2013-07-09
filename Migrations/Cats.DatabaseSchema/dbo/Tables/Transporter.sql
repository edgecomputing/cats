CREATE TABLE [dbo].[Transporter] (
    [TransporterID]   INT           IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (50) NOT NULL,
    [NameAM]          NVARCHAR (50) NULL,
    [LongName]        NVARCHAR (50) NULL,
    [BiddingSystemID] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Transporter] PRIMARY KEY CLUSTERED ([TransporterID] ASC)
);

