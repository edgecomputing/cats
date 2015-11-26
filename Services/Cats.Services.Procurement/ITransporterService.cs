using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public interface ITransporterService
    {

        bool AddTransporter(Transporter transporter);
        bool DeleteTransporter(Transporter transporter);
        bool DeleteById(int id);
        bool EditTransporter(Transporter transporter);
        Transporter FindById(int id);
        List<Transporter> GetAllTransporter();
        List<Transporter> FindBy(Expression<Func<Transporter, bool>> predicate);
        List<BidWinner> GetBidWinner(int sourceID, int DestinationID,int bidId);

        BidWinner GetCurrentBidWinner(int sourceID, int DestincationID,int bidId);
    }
}