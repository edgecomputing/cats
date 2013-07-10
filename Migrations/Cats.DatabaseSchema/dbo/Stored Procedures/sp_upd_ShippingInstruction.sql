

CREATE PROCEDURE [sp_upd_ShippingInstruction] (
  @ShippingInstructionID int,
  @Value nvarchar(50)
)
AS
  UPDATE [dbo].[ShippingInstruction] SET
    [Value] = @Value
  WHERE 
    ([ShippingInstructionID] = @ShippingInstructionID)