
CREATE PROCEDURE [sp_sel_GiftCertificate]
AS
  SELECT 
    [GiftCertificateID],
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
  FROM 
    [dbo].[GiftCertificate]