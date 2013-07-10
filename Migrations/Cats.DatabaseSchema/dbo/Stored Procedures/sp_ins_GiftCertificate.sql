

CREATE PROCEDURE [sp_ins_GiftCertificate] (
  @GiftDate datetime,
  @DonorID int,
  @SINumber nvarchar(50),
  @ReferenceNo nvarchar(50),
  @Vessel nvarchar(50),
  @ETA date,
  @IsPrinted bit,
  @ProgramID int,
  @DModeOfTransport int,
  @PortName nvarchar(50)
)
AS
  INSERT INTO [dbo].[GiftCertificate] (
    [GiftDate],
    [DonorID],
    [SINumber],
    [ReferenceNo],
    [Vessel],
    [ETA],
    [IsPrinted],
    [ProgramID],
    [DModeOfTransport],
    [PortName]
  )
  VALUES (
    @GiftDate,
    @DonorID,
    @SINumber,
    @ReferenceNo,
    @Vessel,
    @ETA,
    @IsPrinted,
    @ProgramID,
    @DModeOfTransport,
    @PortName
  )