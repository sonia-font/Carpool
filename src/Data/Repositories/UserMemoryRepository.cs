using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserMemoryRepository : IUserRepository
    {
        Dictionary<string, UserData> users = new Dictionary<string, UserData>();

        public bool CreateUser(UserData userData)
        {
            if (!users.ContainsKey(userData.UserName))
            {
                users.Add(userData.UserName, userData);
            }
            
            return users.ContainsKey(userData.UserName);
        }

        public UserData RetrieveUser(string userName)
        {
            UserData userData = null;

            if (users.ContainsKey(userName))
            {
                userData = users[userName];
            }

            return userData;
        }

        public bool UpdateUser(UserData userData)
        {
            if (users.ContainsKey(userData.UserName))
            {
                users[userData.UserName] = userData;
            }

            return true;
        }
    }
}
