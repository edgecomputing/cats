

CREATE PROCEDURE [sp_upd_Donor] (
  @DonorID int,
  @Name nvarchar(50),
  @IsResponsibleDonor bit,
  @IsSourceDonor bit,
  @LongName nvarchar(500)
)
AS
  UPDATE [dbo].[Donor] SET
    [Name] = @Name,
    [IsResponsibleDonor] = @IsResponsibleDonor,
    [IsSourceDonor] = @IsSourceDonor,
    [LongName] = @LongName
  WHERE 
    ([DonorID] = @DonorID)