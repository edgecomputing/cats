using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public int FDPID { get; set; }
        public virtual FDP FDP { get; set; }
    }
}