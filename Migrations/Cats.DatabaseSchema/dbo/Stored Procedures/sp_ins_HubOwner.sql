

CREATE PROCEDURE [sp_ins_HubOwner] (
  @Name nvarchar(50),
  @LongName nvarchar(500)
)
AS
  INSERT INTO [dbo].[HubOwner] (
    [Name],
    [LongName]
  )
  VALUES (
    @Name,
    @LongName
  )