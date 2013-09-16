CREATE TABLE [dbo].[BidCandidateTransporter] (
    [BidCandidateTransporterID] INT NOT NULL,
    [BidID]                     INT NOT NULL,
    CONSTRAINT [PK_BidCandidateTransporter] PRIMARY KEY CLUSTERED ([BidCandidateTransporterID] ASC),
    CONSTRAINT [FK_BidCandidateTransporter_Transporter] FOREIGN KEY ([BidCandidateTransporterID]) REFERENCES [dbo].[Transporter] ([TransporterID])
);

