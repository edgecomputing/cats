using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using System.Linq;
using Cats.Models.Constant;

namespace Cats.Services.Procurement
{

    public class TransporterService : ITransporterService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TransporterService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddTransporter(Transporter transporter)
        {
            _unitOfWork.TransporterRepository.Add(transporter);
            _unitOfWork.Save();
            return true;

        }
        public bool EditTransporter(Transporter transporter)
        {
            _unitOfWork.TransporterRepository.Edit(transporter);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteTransporter(Transporter transporter)
        {
            if (transporter == null) return false;
            _unitOfWork.TransporterRepository.Delete(transporter);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TransporterRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TransporterRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Transporter> GetAllTransporter()
        {
            return _unitOfWork.TransporterRepository.GetAll();
        }
        public Transporter FindById(int id)
        {
            return _unitOfWork.TransporterRepository.FindById(id);
        }
        public List<Transporter> FindBy(Expression<Func<Transporter, bool>> predicate)
        {
            return _unitOfWork.TransporterRepository.FindBy(predicate);
        }
        #endregion

       
        public List<BidWinner> GetBidWinner(int sourceID, int DestinationID)
        {
            List<BidWinner> Winners = new List<BidWinner>();
          
            var bidWinner =
                _unitOfWork.BidWinnerRepository.Get(
                    t => t.SourceID == sourceID && t.DestinationID == DestinationID && t.Position == 1 &&
                        t.Bid.StatusID == 5).FirstOrDefault();
           
            if (bidWinner == null)
            {
                return Winners;
            }
            var bidIdstr = bidWinner.BidID.ToString();
            if (bidIdstr=="")
            {
                return Winners;
            }
            if (bidIdstr != "")
            {
                var currentBidId = int.Parse(bidIdstr);
                Winners = _unitOfWork.BidWinnerRepository.FindBy(q => q.BidID == currentBidId && q.SourceID == sourceID && q.DestinationID == DestinationID && q.Position == 1);
                Winners.OrderBy(t => t.Position);
            }
            return Winners.OrderBy(t => t.Position).ToList();
        }

       
        public BidWinner GetCurrentBidWinner(int sourceID,int DestincationID)
        {
           var winners =GetBidWinner(sourceID, DestincationID);
           if (winners.Count < 1) return null;
            return winners[0];
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


