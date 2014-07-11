

CREATE PROCEDURE [sp_ins_Donor] (
  @Name nvarchar(50),
  @IsResponsibleDonor bit,
  @IsSourceDonor bit,
  @LongName nvarchar(500)
)
AS
  INSERT INTO [dbo].[Donor] (
    [Name],
    [IsResponsibleDonor],
    [IsSourceDonor],
    [LongName]
  )
  VALUES (
    @Name,
    @IsResponsibleDonor,
    @IsSourceDonor,
    @LongName
  )