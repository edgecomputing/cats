

CREATE PROCEDURE [sp_del_ShippingInstruction] (
  @ShippingInstructionID int
)
AS
  DELETE FROM [dbo].[ShippingInstruction]
  WHERE 
    ([ShippingInstructionID] = @ShippingInstructionID)