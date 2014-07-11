

CREATE PROCEDURE [sp_upd_GiftCertificate] (
  @GiftCertificateID int,
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
  UPDATE [dbo].[GiftCertificate] SET
    [GiftDate] = @GiftDate,
    [DonorID] = @DonorID,
    [SINumber] = @SINumber,
    [ReferenceNo] = @ReferenceNo,
    [Vessel] = @Vessel,
    [ETA] = @ETA,
    [IsPrinted] = @IsPrinted,
    [ProgramID] = @ProgramID,
    [DModeOfTransport] = @DModeOfTransport,
    [PortName] = @PortName
  WHERE 
    ([GiftCertificateID] = @GiftCertificateID)