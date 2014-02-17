using Cats.Cats.Data.Micro;

namespace Cats.Data.Micro.Models
{
    public class UserProfile : DynamicModel
    {
        //you don't have to specify the connection - Massive will use the first one it finds in your config
        public UserProfile() : base("CatsContext", "UserProfile", "UserProfileID") { }
    }
}