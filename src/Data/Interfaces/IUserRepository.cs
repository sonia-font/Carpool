using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUserRepository
    {
        bool CreateUser(UserData userData);
        UserData RetrieveUser(string userName); //id natural
        bool UpdateUser(UserData userData);

        //eliminar usuario?
    }
}
