using Cats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Services.Common
{
    public interface IDocumentService
    {
        GiftCertificate GetGiftCertificate(int certificateId);
        TransportOrder GetTransportOrder(int transportOrderId);
    }

}
