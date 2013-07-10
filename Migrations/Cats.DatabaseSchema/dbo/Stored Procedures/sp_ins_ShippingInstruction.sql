

CREATE PROCEDURE [sp_ins_ShippingInstruction] (
  @Value nvarchar(50)
)
AS
  INSERT INTO [dbo].[ShippingInstruction] (
    [Value]
  )
  VALUES (
    @Value
  )