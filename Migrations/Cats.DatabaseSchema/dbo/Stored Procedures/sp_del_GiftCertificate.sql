

CREATE PROCEDURE [sp_del_GiftCertificate] (
  @GiftCertificateID int
)
AS
  DELETE FROM [dbo].[GiftCertificate]
  WHERE 
    ([GiftCertificateID] = @GiftCertificateID)