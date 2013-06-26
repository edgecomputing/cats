using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace EWdispatchAllocationList
{
    class AllocationViewModel
    {
        public AllocationModelDetail _detail;

        public AllocationViewModel()
        {
           

        }

        public AllocationViewModel(AllocationModelDetail allocationViewModelDetail)

        {
            this._detail = allocationViewModelDetail;

        }



        public int? FDPID
        {
            get { return _detail.FDPID; }
            set { _detail.FDPID = value; }
        }

        public int? Beneficiaries
        {
            get { return _detail.Beneficiaries; }
            set
            {
                _detail.Beneficiaries = value;
                //OnPropertyChanged("Beneficiaries");
            }
        }

        public string Name
        {
            get { return _detail.FDPName; }
            set { _detail.FDPName = value; }
        }

        public string Woreda
        {
            get { return _detail.Woreda; }
            set { _detail.Woreda = value; }
        }

        public string Zone
        {
            get { return _detail.Zone; }
            set { _detail.Zone = value; }
        }

        public string Region
        {
            get { return _detail.Region; }
            set { _detail.Region = value; }
        }
    }
}
