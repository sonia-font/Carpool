using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class User
    {
        public string UserName { get; set; }
         
        public string Email { get; set; }

        public string FullName { get; set; }

        public Profile Profile { get; set; }
    }
}
