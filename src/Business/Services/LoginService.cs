using Business.Entities;
using Business.Interfaces;
using Data;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class LoginService : ILoginService
    {
        IUserRepository userRepository;        

        public LoginService(IUserRepository userRepository) //dependencias
        {
            this.userRepository = userRepository;

            CreateUser("admin", "todobien", "sonia.font@hotmail.com", "Sonia Font");
            CreateUser("otro", "todomaso", "otro.tipo@hotmail.com", "Otro Tipo");
            Login("admin", "todobien", Profile.Driver);
        }
       

        public User CurrentUser { get; private set; }

        public void ChangePassword(string userName, string currentPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName)); //("userName")
            }            
            if (newPassword == currentPassword)
            {
                throw new ArgumentException("New Password must be different from current Passsword", nameof(newPassword));
            }

            ValidatePassword(currentPassword);
            ValidatePassword(newPassword);

            
            UserData userData = userRepository.RetrieveUser(userName);

            if (userData == null)
            {
                throw new Exception("Usuario debe existir");
            }
            
            if (userData.Password == currentPassword)
            {
                userData.Password = newPassword;

                userRepository.UpdateUser(userData);                    
            }                            
        }

        public void CreateUser(string userName, string password, string email, string fullName)  //validate argument
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }            
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            ValidatePassword(password);


            UserData userData = userRepository.RetrieveUser(userName); //metodo que pregunte si existe userdata dentro de la clase

            if (userData != null)
            {
                throw new Exception("El usuario ya existe");
            }

            userData = new UserData();

            userData.UserName = userName;
            userData.Password = password;
            userData.Email = email;
            userData.FullName = fullName;                

            userRepository.CreateUser(userData);                      
        }

        public bool Login(string userName, string password, Profile profile)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }      

            UserData userData = userRepository.RetrieveUser(userName);

            if (userData == null)
            {
                throw new Exception("El usuario no existe");
            }
            
            if (userData.Password == password)
            {
                User user = new User();

                user.Email = userData.Email;
                user.FullName = userData.FullName;                    
                user.UserName = userData.UserName;

                CurrentUser = user;
                CurrentUser.Profile = profile;

                return true;
            }
            else
            {
                return false;
            }         
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (password.Length < 8)
            {
                throw new ArgumentException("La contraseña debe tener 8 o mas caracteres", nameof(password));
            }            
        }
    }
}
