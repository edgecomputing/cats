CREATE TABLE [dbo].[RationDetail] (
    [RationDetailID] INT              IDENTITY (1, 1) NOT NULL,
    [RationID]       INT              NOT NULL,
    [CommodityID]    INT              NOT NULL,
    [Rate]           FLOAT (53)       NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RationRateDetail] PRIMARY KEY CLUSTERED ([RationDetailID] ASC),
    CONSTRAINT [FK_RationRateDetail_RationRate] FOREIGN KEY ([RationID]) REFERENCES [dbo].[Ration] ([RationID])
);

