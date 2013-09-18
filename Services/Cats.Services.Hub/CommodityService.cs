

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.Hub;
using Cats.Models.Hub;
using Cats.Models.Hub.ViewModels.Common;


namespace Cats.Services.Hub
{

    public class CommodityService : ICommodityService
    {
        private readonly IUnitOfWork _unitOfWork;


        public CommodityService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddCommodity(Commodity commodity)
        {
            _unitOfWork.CommodityRepository.Add(commodity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditCommodity(Commodity commodity)
        {
            _unitOfWork.CommodityRepository.Edit(commodity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteCommodity(Commodity commodity)
        {
            if (commodity == null) return false;
            _unitOfWork.CommodityRepository.Delete(commodity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.CommodityRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.CommodityRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Commodity> GetAllCommodity()
        {
            return _unitOfWork.CommodityRepository.GetAll();
        }
        public Commodity FindById(int id)
        {
            return _unitOfWork.CommodityRepository.FindById(id);
        }
        public List<Commodity> FindBy(Expression<Func<Commodity, bool>> predicate)
        {
            return _unitOfWork.CommodityRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }


        /// <summary>
        /// Gets all parents.
        /// </summary>
        /// <returns></returns>
        public List<Commodity> GetAllParents()
        {
            return _unitOfWork.CommodityRepository.FindBy(t => t.ParentID == null);
        }
        /// <summary>
        /// Gets all sub commodities.
        /// </summary>
        /// <returns></returns>
        public List<Commodity> GetAllSubCommodities()
        {
            return _unitOfWork.CommodityRepository.FindBy(t => t.ParentID != null);
        }
        /// <summary>
        /// Gets the name of the commodity by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Commodity GetCommodityByName(string name)
        {
            return _unitOfWork.CommodityRepository.FindBy(t => t.Name == name).FirstOrDefault();
        }
        /// <summary>
        /// Gets all sub commodities by parant id.
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public List<Commodity> GetAllSubCommoditiesByParantId(int Id)
        {
            return _unitOfWork.CommodityRepository.FindBy(t => t.ParentID == Id);
           
        }
        /// <summary>
        /// Determines whether name is valid for the specified commodity ID.
        /// </summary>
        /// <param name="CommodityID">The commodity ID.</param>
        /// <param name="Name">The name.</param>
        /// <returns>
        ///   <c>true</c> if  [name is valid] for [the specified commodity ID]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNameValid(int? CommodityID, string Name)
        {
            return !_unitOfWork.CommodityRepository.FindBy(t => t.Name == Name && t.CommodityID != CommodityID).Any();
           
        }
        /// <summary>
        /// Determines whether [commodity code is valid] for [the specified commodity ID].
        /// </summary>
        /// <param name="CommodityID">The commodity ID.</param>
        /// <param name="CommodityCode">The commodity code.</param>
        /// <returns>
        ///   <c>true</c> if [commodity code is valid] for [the specified commodity ID]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCodeValid(int? CommodityID, string CommodityCode)
        {
            return
                !_unitOfWork.CommodityRepository.FindBy(
                    t => t.CommodityCode == CommodityCode && t.CommodityID != CommodityID).Any();

           
        }
        /// <summary>
        /// GetAllCommodityForReport
        /// </summary>
        /// <returns></returns>
        public List<CommodityViewModel> GetAllCommodityForReprot()
        {
            var tempComodities = _unitOfWork.CommodityRepository.FindBy(t => t.ParentID == null);
            var commodities = (from c in tempComodities  select new CommodityViewModel() { CommodityId = c.CommodityID, CommodityName = c.Name }).ToList();
            commodities.Insert(0, new CommodityViewModel { CommodityName = "All Commodities" });
            return commodities;
        }
    }
}


