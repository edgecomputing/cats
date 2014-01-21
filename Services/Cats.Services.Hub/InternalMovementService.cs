

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.Hub;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;


namespace Cats.Services.Hub
{

    public class InternalMovementService : IInternalMovementService
    {
        private readonly IUnitOfWork _unitOfWork;


        public InternalMovementService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddInternalMovement(InternalMovement entity)
        {
            _unitOfWork.InternalMovementRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditInternalMovement(InternalMovement entity)
        {
            _unitOfWork.InternalMovementRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteInternalMovement(InternalMovement entity)
        {
            if (entity == null) return false;
            _unitOfWork.InternalMovementRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.InternalMovementRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.InternalMovementRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<InternalMovementLogViewModel> GetAllInternalMovmentLog()
        {
            List<InternalMovementLogViewModel> internalMovmentLogViewModel = new List<InternalMovementLogViewModel>();
            InternalMovementLogViewModel _internalMovment;
            var movements = _unitOfWork.InternalMovementRepository.GetAll();

            var internalMovments = (from c in movements select c).ToList();

            foreach (var internalMovment in internalMovments)
            {
                foreach (var transaction in internalMovment.TransactionGroup.Transactions)
                {
                    if (transaction.TransactionGroupID == internalMovment.TransactionGroupID)
                    {
                        if (transaction.QuantityInMT >= 0)
                        {
                            var fromStore = _unitOfWork.TransactionRepository.FindBy(c=>c.TransactionGroupID == internalMovment.TransactionGroupID && c.QuantityInMT < 0).Select(q=>q.Store.Name + " - " + q.Store.StoreManName).FirstOrDefault();
                            var fromStack = _unitOfWork.TransactionRepository.FindBy(c => c.TransactionGroupID == internalMovment.TransactionGroupID && c.QuantityInMT < 0).Select(q => q.Stack.Value).FirstOrDefault();
                            var commodity = _unitOfWork.CommodityRepository.FindBy(c=>c.CommodityID == transaction.CommodityID).Select(c => c.Name).Single();

                            _internalMovment = new InternalMovementLogViewModel();
                            _internalMovment.TransactionId = transaction.TransactionID;
                            _internalMovment.FromStore = fromStore;// (from c in db.Transactions where ((c.TransactionGroupID == internalMovment.TransactionGroupID) && (c.QuantityInMT < 0)) select (c.Store.Name + " - " + c.Store.StoreManName)).FirstOrDefault();
                            _internalMovment.FromStack = fromStack;// (from c in db.Transactions where ((c.TransactionGroupID == internalMovment.TransactionGroupID) && (c.QuantityInMT < 0)) select c.Stack.Value).FirstOrDefault();
                            _internalMovment.SelectedDate = internalMovment.TransferDate;
                            _internalMovment.ToStore = string.Format("{0} - {1} ", transaction.Store.Name, transaction.Store.StoreManName);
                            _internalMovment.ToStack = transaction.Stack.Value;
                            _internalMovment.RefernaceNumber = internalMovment.ReferenceNumber;
                            _internalMovment.CommodityName = commodity;// repository.Commodity.FindById(transaction.CommodityID).Name;
                            _internalMovment.Program = transaction.Program.Name;
                            _internalMovment.ProjectCodeName = transaction.ProjectCode.Value;
                            _internalMovment.ShippingInstructionNumber = transaction.ShippingInstruction.Value;
                            _internalMovment.Unit = transaction.Unit.Name;
                            _internalMovment.QuantityInUnit = transaction.QuantityInUnit;
                            _internalMovment.QuantityInMt = transaction.QuantityInMT;
                            _internalMovment.Reason = internalMovment.Detail.Description;
                            _internalMovment.Note = internalMovment.Notes;
                            _internalMovment.ApprovedBy = internalMovment.ApprovedBy;
                            _internalMovment.Reason = internalMovment.Detail.Name;
                            internalMovmentLogViewModel.Add(_internalMovment);
                        }
                    }
                }
            }

            return internalMovmentLogViewModel;
        }
        public InternalMovement FindById(int id)
        {
            return _unitOfWork.InternalMovementRepository.FindById(id);
        }
        public List<InternalMovement> FindBy(Expression<Func<InternalMovement, bool>> predicate)
        {
            return _unitOfWork.InternalMovementRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }





        public void AddNewInternalMovement(InternalMovementViewModel viewModel, UserProfile user)
        {

          //  _unitOfWork.TransactionRepository.SaveInternalMovementTrasnsaction(viewModel, user);
        }

       
    }
}


