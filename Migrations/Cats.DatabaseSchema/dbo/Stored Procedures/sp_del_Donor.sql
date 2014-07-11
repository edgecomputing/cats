

CREATE PROCEDURE [sp_del_Donor] (
  @DonorID int
)
AS
  DELETE FROM [dbo].[Donor]
  WHERE 
    ([DonorID] = @DonorID)