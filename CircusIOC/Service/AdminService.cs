using CircusIOC.Models;
using CircusIOC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircusIOC.Service
{
    public class AdminService
    {
        //public IAdminRepository AdminRepository { get; private set; }
        //public AdminService(IAdminRepository adminRepository)
        //{
        //    this.AdminRepository = adminRepository;
        //}
        public static Admins GetByNamePsw(string userName, string psw)
        {
            return Repository.AdminRepository.GetByNamePsw(userName, psw);
        }

    }
}