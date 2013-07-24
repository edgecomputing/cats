using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using System.Linq;

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

        public List<TransportBidQuotation> GetBidWinner(int sourceID, int DestinationID)
        {
            List<TransportBidQuotation> Winners = new List<TransportBidQuotation>();
            List<ApplicationSetting> currentBids = _unitOfWork.ApplicationSettingRepository.FindBy(t => t.SettingName == "CurrentBid");
            if (currentBids.Count < 1)
            {
                return Winners;
            }
            string bidIdstr = currentBids[0].SettingValue;
            if (bidIdstr=="")
            {
                return Winners;
            }
            if (bidIdstr != "")
            {
                int currentBidId = int.Parse(bidIdstr);
                Winners = _unitOfWork.TransportBidQuotationRepository.FindBy(q => q.BidID == currentBidId && q.SourceID == sourceID && q.DestinationID == DestinationID && q.IsWinner == true);
                Winners.OrderBy(t => t.Position);
            }
            return Winners;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


