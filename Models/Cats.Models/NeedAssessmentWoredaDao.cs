using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class NeedAssessmentWoredaDao
    {
        public int Woreda  { get; set; }
        public string WoredaName { get; set; }
        public int Zone { get; set; }
        public string ZoneName {get; set; }
        public int NeedAID  { get; set; }

        public int NAId { get; set; }
        [Range(0, Int32.MaxValue)]

      
        
        public Nullable<int> ProjectedMale { get; set; }
        
         [Range(0, Int32.MaxValue)]
        public Nullable<int> ProjectedFemale { get; set; }
        
         [Range(0, Int32.MaxValue)]
        public Nullable<int> RegularPSNP { get; set; }
        

         [Range(0, Int32.MaxValue)]
        public Nullable<int> PSNP { get; set; }

        
         [Range(0, Int32.MaxValue)]
        public Nullable<int> NonPSNP { get; set; }
         
      
         [Range(0, Int32.MaxValue)]
        public Nullable<int> Contingencybudget { get; set; }


        
         [Range(0, Int32.MaxValue)]
        public Nullable<int> TotalBeneficiaries { get; set; }

       
        [Range(0, Int32.MaxValue)]
        public Nullable<int> PSNPFromWoredasMale { get; set; }


        [Range(0, Int32.MaxValue)]
        public Nullable<int> PSNPFromWoredasFemale { get; set; }

        
        [Range(0, Int32.MaxValue)]
        public Nullable<int> PSNPFromWoredasDOA { get; set; }

         //[DataType(DataType.Currency)]
        [Range(0, Int32.MaxValue)]
        public Nullable<int> NonPSNPFromWoredasMale { get; set; }


        [Range(0, Int32.MaxValue)]
        public Nullable<int> NonPSNPFromWoredasFemale { get; set; }

       
        [Range(0, Int32.MaxValue)]
        public Nullable<int> NonPSNPFromWoredasDOA { get; set; }

         public Nullable<double> Total { get; set; }                           
                                                             
    }
}
