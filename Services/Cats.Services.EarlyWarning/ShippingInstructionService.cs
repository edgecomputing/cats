using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public class ShippingInstructionService :IShippingInstructionService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ShippingInstructionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddShippingInstruction(ShippingInstruction shippingInstruction)
        {
            _unitOfWork.ShippingInstructionRepository.Add(shippingInstruction);
            _unitOfWork.Save();
            return true;

        }
        public bool EditShippingInstruction(ShippingInstruction shippingInstruction)
        {
            _unitOfWork.ShippingInstructionRepository.Edit(shippingInstruction);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteShippingInstruction(ShippingInstruction shippingInstruction)
        {
            if (shippingInstruction == null) return false;
            _unitOfWork.ShippingInstructionRepository.Delete(shippingInstruction);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ShippingInstructionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ShippingInstructionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<ShippingInstruction> GetAllShippingInstruction()
        {
            return _unitOfWork.ShippingInstructionRepository.GetAll();
        }
        public ShippingInstruction FindById(int id)
        {
            return _unitOfWork.ShippingInstructionRepository.FindById(id);
        }
        public List<ShippingInstruction> FindBy(Expression<Func<ShippingInstruction, bool>> predicate)
        {
            return _unitOfWork.ShippingInstructionRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

        public ShippingInstruction GetSiNumber(string siNumber)
        {
            var oldSiNumber = _unitOfWork.ShippingInstructionRepository.FindBy(m => m.Value == siNumber).SingleOrDefault();
            if (oldSiNumber == null)
            {
                var shippingInstruction = new ShippingInstruction()
                {
                    Value = siNumber
                };
                _unitOfWork.ShippingInstructionRepository.Add(shippingInstruction);
                _unitOfWork.Save();
                return _unitOfWork.ShippingInstructionRepository.FindBy(m => m.Value == siNumber).SingleOrDefault();
            }
            return oldSiNumber;
        }
    }
}
