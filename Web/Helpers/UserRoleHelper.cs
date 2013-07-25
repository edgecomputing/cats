using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Helpers
{
    // TODO: This class is only for demo purposes and by no means supposed to replicate or replace user authorization
    public class UserRoleHelper
    {
        public static string GetUserRole(string userName)
        {
            var role = "GU";
            switch (userName.ToUpper())
            {
                case "ADMIN":
                    role = "SU";
                    break;
                case "LOGISTICS":
                    role = "LG";
                    break;
                case "EARLYWARNING":
                    role = "EW";
                    break;
                case "PROCUREMENT":
                    role = "PC";
                    break;
            }
            return role;
        }
    }
}