using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAbscences.Services
{

    public interface IAdminService
    {
        bool Login(int Id, string Password);
        bool ChangePassword(int Id, string Password);
        bool ForgotPassword(int Id);

    }

    public class AdminService : IAdminService
    {

        public GestionAbscencesEntities11 context { get; set; }

        public AdminService()
        {

            context = new GestionAbscencesEntities11();
        }

        public bool Login(int id, string Password)
        {
            return context.employe.Where(a => a.idEmploye == id && a.password == Password).Any();
        }

        public bool ChangePassword(int Id, string Password)
        {
            throw new NotImplementedException();
        }

        public bool ForgotPassword(int Id)
        {
            throw new NotImplementedException();
        }


    }
}