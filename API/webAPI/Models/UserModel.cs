using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class UserModel
    {
        public static RV_User GetUser(string email, ArvinoDbContext db)
        {
            return db.RV_User.SingleOrDefault(x => x.email == email);
        }
    }
}