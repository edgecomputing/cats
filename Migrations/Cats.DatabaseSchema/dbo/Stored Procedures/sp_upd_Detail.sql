

CREATE PROCEDURE [sp_upd_Detail] (
  @DetailID int,
  @Name nvarchar(50),
  @Description nvarchar(500),
  @MasterID int,
  @SortOrder int
)
AS
  UPDATE [dbo].[Detail] SET
    [Name] = @Name,
    [Description] = @Description,
    [MasterID] = @MasterID,
    [SortOrder] = @SortOrder
  WHERE 
    ([DetailID] = @DetailID)