

CREATE PROCEDURE [sp_ins_Audit] (
  @LoginID int,
  @DateTime datetime,
  @Action char,
  @TableName varchar(30),
  @PrimaryKey varchar(30),
  @ColumnName varchar(3000),
  @NewValue varchar(3000),
  @OldValue varchar(3000)
)
AS
  INSERT INTO [dbo].[Audit] (
    [LoginID],
    [DateTime],
    [Action],
    [TableName],
    [PrimaryKey],
    [ColumnName],
    [NewValue],
    [OldValue]
  )
  VALUES (
    @LoginID,
    @DateTime,
    @Action,
    @TableName,
    @PrimaryKey,
    @ColumnName,
    @NewValue,
    @OldValue
  )