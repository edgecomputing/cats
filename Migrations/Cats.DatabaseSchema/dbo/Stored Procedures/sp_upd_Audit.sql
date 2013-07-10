

CREATE PROCEDURE [sp_upd_Audit] (
  @AuditID int,
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
  UPDATE [dbo].[Audit] SET
    [LoginID] = @LoginID,
    [DateTime] = @DateTime,
    [Action] = @Action,
    [TableName] = @TableName,
    [PrimaryKey] = @PrimaryKey,
    [ColumnName] = @ColumnName,
    [NewValue] = @NewValue,
    [OldValue] = @OldValue
  WHERE 
    ([AuditID] = @AuditID)