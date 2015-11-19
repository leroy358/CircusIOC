using CircusIOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircusIOC.Repository
{
    public class AdminRepository
    {
        private static MaXiDbContext db = new MaXiDbContext();
        public static Admins GetByNamePsw(string userName, string psw)
        {
            return db.Admins.Where(item => item.AdminName == userName && item.Password == psw).First();
        }
    }
}