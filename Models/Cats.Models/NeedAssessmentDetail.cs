using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class NeedAssessmentDetail
    {
        public int NAId { get; set; }
        public Nullable<int> NeedAId { get; set; }
        public Nullable<int> Woreda { get; set; }

        [Display(Name = "Project Male")]
        [Required(ErrorMessage = "Project Male is Required")]
        [Range(0,Double.MaxValue,ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> ProjectedMale { get; set; }


         [Display(Name = "Project Female")]
         [Required(ErrorMessage = "Project Female is Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> ProjectedFemale { get; set; }


         [Display(Name = "Regular PSNP")]
         [Required(ErrorMessage = "Regular PSNP is Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> RegularPSNP { get; set; }


         [Display(Name = "PSNP")]
         [Required(ErrorMessage = "PSNP is Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> PSNP { get; set; }


         [Display(Name = "Non PSNP")]
         [Required(ErrorMessage = "Non PSNP is  Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> NonPSNP { get; set; }


         [Display(Name = "Contigency Budget")]
         [Required(ErrorMessage = "Contigency Budget is  Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> Contingencybudget { get; set; }


         [Display(Name = "Total Beneficiaries")]
         [Required(ErrorMessage = "Total Beneficiaries is  Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> TotalBeneficiaries { get; set; }


         [Display(Name = "PSNP From Woreda Male")]
         [Required(ErrorMessage = "PSNP From Woreda Male is  Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> PSNPFromWoredasMale { get; set; }


         [Display(Name = "PSNP From Woreda Female")]
         [Required(ErrorMessage = "PSNP From Woreda Female is  Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> PSNPFromWoredasFemale { get; set; }


         [Display(Name = "PSNP From Woreda DOA")]
         [Required(ErrorMessage = "PSNP From Woreda DOA is  Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> PSNPFromWoredasDOA { get; set; }


         [Display(Name = "Non PSNP From Woreda Male")]
         [Required(ErrorMessage = "Non PSNP From Woreda Male is  Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> NonPSNPFromWoredasMale { get; set; }


         [Display(Name = "Non PSNP From Woreda Female")]
         [Required(ErrorMessage = " Non PSNP From Woreda Female is  Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> NonPSNPFromWoredasFemale { get; set; }


         [Display(Name = "Non PSNP From Woreda DOA")]
         [Required(ErrorMessage = "Non PSNP From Woreda DOA is  Required")]
         [Range(0, Double.MaxValue, ErrorMessage = "Minimum value allowed is 0")]
        public Nullable<int> NonPSNPFromWoredasDOA { get; set; }


         [Display(Name = "Remark")]
        public string Remark { get; set; }
         
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual NeedAssessmentHeader NeedAssessmentHeader { get; set; }
    }
}
