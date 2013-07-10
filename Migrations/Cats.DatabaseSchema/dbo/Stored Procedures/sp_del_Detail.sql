

CREATE PROCEDURE [sp_del_Detail] (
  @DetailID int
)
AS
  DELETE FROM [dbo].[Detail]
  WHERE 
    ([DetailID] = @DetailID)