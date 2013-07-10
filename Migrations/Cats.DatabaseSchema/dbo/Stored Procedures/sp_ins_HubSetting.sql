

CREATE PROCEDURE [sp_ins_HubSetting] (
  @Key nvarchar(50),
  @Name nvarchar(50),
  @ValueType nvarchar(50),
  @Options nvarchar(500)
)
AS
  INSERT INTO [dbo].[HubSetting] (
    [Key],
    [Name],
    [ValueType],
    [Options]
  )
  VALUES (
    @Key,
    @Name,
    @ValueType,
    @Options
  )