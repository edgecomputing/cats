CREATE TABLE [dbo].[ShippingInstruction] (
    [ShippingInstructionID] INT           IDENTITY (1, 1) NOT NULL,
    [Value]                 NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ShippingInstruction] PRIMARY KEY CLUSTERED ([ShippingInstructionID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ShippingInstruction]
    ON [dbo].[ShippingInstruction]([Value] ASC);

