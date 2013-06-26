using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EWdispatchAllocationList
{
    using System.Runtime.Serialization;
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    //[System.Runtime.Serialization.DataContractAttribute(Name = "AllocationRequestDetailModel", Namespace = "http://schemas.datacontract.org/2004/07/CATS.Services.DataContracts")]
    public class AllocationModelDetail 
    {

        private string ItemName;

        private int RequisitionNo;
        private string Doners;
        private System.Nullable<int> BeneficiariesField;

        private System.Nullable<int> FDPIDField;

        private int Amount;

        private string FDPNameField;
        private string RegionField;
        private string WoredaField;
        private string ZoneField;
        private int RelifeRequestID;
        private int AdminUnitID;


        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Beneficiaries
        {
            get
            {
                return this.BeneficiariesField;
            }
            set
            {
                if ((this.BeneficiariesField.Equals(value) != true))
                {
                    this.BeneficiariesField = value;
                    this.RaisePropertyChanged("Beneficiaries");
                }
            }
        }
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> FDPID
        {
            get
            {
                return this.FDPIDField;
            }
            set
            {
                if ((this.FDPIDField.Equals(value) != true))
                {
                    this.FDPIDField = value;
                    this.RaisePropertyChanged("FDPID");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FDPName
        {
            get
            {
                return this.FDPNameField;
            }
            set
            {
                if ((object.ReferenceEquals(this.FDPNameField, value) != true))
                {
                    this.FDPNameField = value;
                    this.RaisePropertyChanged("FDPName");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Region
        {
            get
            {
                return this.RegionField;
            }
            set
            {
                if ((object.ReferenceEquals(this.RegionField, value) != true))
                {
                    this.RegionField = value;
                    this.RaisePropertyChanged("Region");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Woreda
        {
            get
            {
                return this.WoredaField;
            }
            set
            {
                if ((object.ReferenceEquals(this.WoredaField, value) != true))
                {
                    this.WoredaField = value;
                    this.RaisePropertyChanged("Woreda");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Zone
        {
            get
            {
                return this.ZoneField;
            }
            set
            {
                if ((object.ReferenceEquals(this.ZoneField, value) != true))
                {
                    this.ZoneField = value;
                    this.RaisePropertyChanged("Zone");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }

        }
    }
}