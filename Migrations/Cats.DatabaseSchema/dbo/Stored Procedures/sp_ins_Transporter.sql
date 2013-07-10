

CREATE PROCEDURE [sp_ins_Transporter] (
  @Name nvarchar(50),
  @LongName nvarchar(50),
  @BiddingSystemID nvarchar(50)
)
AS
  INSERT INTO [dbo].[Transporter] (
    [Name],
    [LongName],
    [BiddingSystemID]
  )
  VALUES (
    @Name,
    @LongName,
    @BiddingSystemID
  )