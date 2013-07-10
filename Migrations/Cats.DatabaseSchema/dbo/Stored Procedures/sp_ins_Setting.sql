

CREATE PROCEDURE [sp_ins_Setting] (
  @Category varchar(100),
  @Key varchar(100),
  @Value varchar(100),
  @Option varchar(100),
  @Type varchar(100)
)
AS
  INSERT INTO [dbo].[Setting] (
    [Category],
    [Key],
    [Value],
    [Option],
    [Type]
  )
  VALUES (
    @Category,
    @Key,
    @Value,
    @Option,
    @Type
  )