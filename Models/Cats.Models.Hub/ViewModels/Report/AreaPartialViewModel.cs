using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub.ViewModels.Report
{
    public class AreaPartialViewModel
    {
        public List<AreaViewModel> Region { get; set; }
        public List<AreaViewModel> Zone { get; set; }
        public List<AreaViewModel> Woreda { get; set; }
        public List<AreaViewModel> FDP { get; set; }


        public AreaPartialViewModel()
        {
        }
        //TODO:commented by banty separation of concern
        //public AreaPartialViewModel(IUnitOfWork Repository, UserProfile user)
        //{
        //    this.Region = Repository.AdminUnit.GetAllAreasForReport();
        //}
    }
}
