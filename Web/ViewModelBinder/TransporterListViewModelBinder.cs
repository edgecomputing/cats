using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Models.ViewModels;
//using DocumentFormat.OpenXml.Wordprocessing;

namespace Cats.ViewModelBinder
{
    public class TransporterListViewModelBinder
    {
        public static IEnumerable<TransporterViewModel> ReturnViewModel(List<Transporter> trasporters)
        {
            return  trasporters.Select(t => new TransporterViewModel()
                                                      {
                                                          TransporterID = t.TransporterID,
                                                          Name = t.Name,
                                                          Region = t.Region,
                                                          SubCity = t.SubCity,
                                                          Zone = t.Zone,
                                                          MobileNo = t.MobileNo,
                                                          Capital = t.Capital
                                                      });
        }
    }
}