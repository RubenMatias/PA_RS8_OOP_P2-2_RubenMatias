using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using D00_Utility;
using RSGymPTManagement_DAL;
using RSGymPTManagement_DAL.Model;

namespace RSGymPTManagement_Client.Configurations
{
    public class DataLogin
    {
        const string headerTitle = "RSGym - Management System";
        //Lê os dados inseridos pelo utilizador
        public static (string, string) ReadCredentials()
        {
            UtilityMenu.ChangePage();

            Utility.WriteTitle("Login");

            Utility.WriteLineMessage("\tPlease enter your credentils to enter!\n\n");

            Utility.WriteMessage("\tCode: ");
            string code = Console.ReadLine();

            Utility.WriteMessage("\n\tPassword:  ");
            string password = Console.ReadLine();


            return (code, password);

        }
        public static (string, string) ValidateCredentials((string, string) credentials)
        {
           

            credentials.Item1 = ValidateCode();
            credentials.Item2 = ValidatePassword();

            string ValidateCode()
            {
                using (var db = new RSGymPTManagementContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Code == credentials.Item1);


                    if (user != null)
                    {
                        Session.Role = user.Role.ToString();
                        Session.Name = user.Name;

                        return Session.Role.ToString();
                    }
                    else
                    {
                        return "Invalid Code";
                    }

                }

            }

            string ValidatePassword()
            {
                using (var db = new RSGymPTManagementContext())
                {
                    var user = db.Users.FirstOrDefault(p => p.Password == credentials.Item2);

                    if (user != null)
                    {
                        user.Password = credentials.Item2;

                        return "password OK";
                    }
                    else
                    {
                        return "Invalid password";
                    }

                }
            }



            return (credentials.Item1, credentials.Item2);
        }

        // Escreve a mensagem de dados errados ou certos 
        public static (string, string) MessageLogin((string, string) credentials)
        {
            using (var db = new RSGymPTManagementContext())
            {

                var user = db.Users.Select(u => u.Name);


                if (credentials.Item1 == "Invalid code" && credentials.Item2 == "Invalid password")
                {
                    Utility.ErrorMessage($" {credentials.Item1} and {credentials.Item2}! Try Again");
                    Console.ReadKey();
                }
                else if (credentials.Item2 == "Invalid password")
                {
                    Utility.ErrorMessage($"{credentials.Item2}! Try Again");
                    Console.ReadKey();
                }
                else if (credentials.Item1 == "Invalid Code")
                {
                    Utility.ErrorMessage($"{credentials.Item1}! Try Again");
                    Console.ReadKey();
                }
                else
                {
                    Utility.ValidationMessage($"\n\tWelcome {Session.Name}!");
                    Console.ReadKey();
                }

                return (credentials.Item1, credentials.Item2);

            }
        }

        public static (string,string) LoopLogin((string, string) credentials) 
        {

            while (credentials.Item1 != "colab" && credentials.Item2 != "password OK" || credentials.Item1 != "admin" && credentials.Item2 != "password OK" || credentials.Item1 == "Invalid code" || credentials.Item2 == "Invalid password" )
            {
                DataLogin.MessageLogin(DataLogin.ValidateCredentials(DataLogin.ReadCredentials()));

            }

            return (credentials.Item1, credentials.Item2);
        }







    }
}
