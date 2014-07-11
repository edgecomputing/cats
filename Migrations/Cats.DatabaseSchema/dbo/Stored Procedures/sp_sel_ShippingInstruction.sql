
CREATE PROCEDURE [sp_sel_ShippingInstruction]
AS
  SELECT 
    [ShippingInstructionID],
    [Value]
  FROM 
    [dbo].[ShippingInstruction]