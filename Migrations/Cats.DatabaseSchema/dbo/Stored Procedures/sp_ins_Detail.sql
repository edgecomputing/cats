

CREATE PROCEDURE [sp_ins_Detail] (
  @Name nvarchar(50),
  @Description nvarchar(500),
  @MasterID int,
  @SortOrder int
)
AS
  INSERT INTO [dbo].[Detail] (
    [Name],
    [Description],
    [MasterID],
    [SortOrder]
  )
  VALUES (
    @Name,
    @Description,
    @MasterID,
    @SortOrder
  )