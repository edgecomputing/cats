using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.Common
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GiftCertificate GetGiftCertificate(int certificateId)
        {
            return _unitOfWork.GiftCertificateRepository.Get(g => g.GiftCertificateID == certificateId).SingleOrDefault();
        }

        public TransportOrder GetTransportOrder(int transportOrderId)
        {
            return
                _unitOfWork.TransportOrderRepository.Get(t => t.TransportOrderID == transportOrderId).SingleOrDefault();
        }
    }
}
