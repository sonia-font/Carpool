using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces

{
    public interface ILoginService
    {
        User CurrentUser { get; }

        void CreateUser(string userName, string password, string email, string fullName);     
        bool Login(string userName, string password, Profile profile);
        void ChangePassword(string userName, string currentPassword, string newPassword);     
        //olvido contraseña
    }
}
