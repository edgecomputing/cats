

CREATE PROCEDURE [sp_del_GiftCertificateDetail] (
  @GiftCertificateDetailID int
)
AS
  DELETE FROM [dbo].[GiftCertificateDetail]
  WHERE 
    ([GiftCertificateDetailID] = @GiftCertificateDetailID)