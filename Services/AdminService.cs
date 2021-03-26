using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAbscences.Services
{

    public interface IAdminService
    {
        bool Login(string Email, string Password);
        bool ChangePassword(string Email, string Password);
        bool ForgotPassword(string Email);
    }

    public class AdminService : IAdminService
    {

        public GestionAbscencesEntities context { get; set; }

        public AdminService() {

        }

        public bool Login(int id, string Password)
        {
            return context.employe.Where(a => a.idEmploye == id && a.password == Password).Any();
        }

        public bool ChangePassword(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public bool ForgotPassword(string Email)
        {
            throw new NotImplementedException();
        }

        bool IAdminService.Login(string Email, string Password)
        {
            throw new NotImplementedException();
        }
    }
}