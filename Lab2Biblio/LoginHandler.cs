using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Biblio
{
    class LoginHandler
    {


        public string loginMessage { get; set; }
        public string passwordMessage { get; set; }

        public List<Account> accounts { get; set; }
        public bool logInsuccess { get; set; }
        public bool isEndFlag { get; set; }
        public bool isGoodFlag { get; set; }

        public LoginHandler()
        {


            this.loginMessage = "";
            this.passwordMessage = "";

            this.isEndFlag = false;
            this.isGoodFlag = true;
            this.logInsuccess = false;

            this.accounts = new List<Account>();

            string userLogin;
            string userPassword;

            System.IO.StreamReader file =
            new System.IO.StreamReader(@"D:\Wojti\STUDIA\semestr5\IO\Lab2\Lab2\Lab2\Accounts.txt");
            while ((userLogin = file.ReadLine()) != null)
            {
                userPassword = file.ReadLine();

                Account temp = new Account(userLogin, userPassword);
                //this.accounts.
                this.accounts.Add(temp);
            }

            foreach (var acc in accounts){
                Console.WriteLine($"Account: {acc.login} {acc.password}");
            }


        }
        public string makeResponse(string message)
        {
            if (message.Length != 0 && message != "q")
            {
                if (this.loginMessage == "")
                {
                    this.isGoodFlag = true;
                    try
                    {
                        this.loginMessage = message;

                        Console.WriteLine($"First message ({this.loginMessage})accepted succesfully.");
                        return $"Enter password:";
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Unable to parse '{message}'(login)");
                        this.isGoodFlag = false;
                        return "Login format is incorrect. Try again!";
                    }

                }
                else if (this.passwordMessage == "")
                {
                    this.isGoodFlag = true;
                    try
                    {
                        this.passwordMessage = message;
                        Console.WriteLine($"Password message ({this.passwordMessage})accepted succesfully.");
                        return "Trying to log in . . .";
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Unable to parse '{message}'(password)");
                        this.isGoodFlag = false;
                        return "Password format  is incorrect. Try again!";

                    }
                }
                else
                {
                    this.isGoodFlag = true;

                    if(tryLogIn())
                    {
                        this.logInsuccess = true;
                        return "Success! Logging in. . . ";
                    }
                    else
                    {
                        this.logInsuccess = false;
                        return "Bad login or password";
                    }
                }

            }
            else return "Closing...";
        }

        private bool tryLogIn()
        {
            foreach(var acc in this.accounts)
            {
                if (this.loginMessage == acc.login && this.passwordMessage == acc.password)
                {
                    this.loginMessage = "";
                    this.passwordMessage = "";
                    return true;
                }
            }
            this.loginMessage = "";
            this.passwordMessage = "";
            return false;
        }

    }


}

