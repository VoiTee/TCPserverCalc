using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Biblio
{
    public class Account
    {
        public string login { get; set; }
        public string password { get; set; }


        public Account(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

    }
}
