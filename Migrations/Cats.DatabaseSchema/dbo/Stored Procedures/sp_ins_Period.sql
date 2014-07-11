

CREATE PROCEDURE [sp_ins_Period] (
  @Year int,
  @Month int
)
AS
  INSERT INTO [dbo].[Period] (
    [Year],
    [Month]
  )
  VALUES (
    @Year,
    @Month
  )