

CREATE PROCEDURE [sp_upd_Transporter] (
  @TransporterID int,
  @Name nvarchar(50),
  @LongName nvarchar(50),
  @BiddingSystemID nvarchar(50)
)
AS
  UPDATE [dbo].[Transporter] SET
    [Name] = @Name,
    [LongName] = @LongName,
    [BiddingSystemID] = @BiddingSystemID
  WHERE 
    ([TransporterID] = @TransporterID)