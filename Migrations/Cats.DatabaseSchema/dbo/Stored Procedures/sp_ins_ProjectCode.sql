

CREATE PROCEDURE [sp_ins_ProjectCode] (
  @Value nvarchar(50)
)
AS
  INSERT INTO [dbo].[ProjectCode] (
    [Value]
  )
  VALUES (
    @Value
  )