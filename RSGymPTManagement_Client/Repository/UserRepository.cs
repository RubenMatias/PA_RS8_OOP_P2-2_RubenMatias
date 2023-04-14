using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RSGymPTManagement_Client.Configurations;
using RSGymPTManagement_DAL;
using RSGymPTManagement_DAL.Model;
using System.Data.Entity;
using D00_Utility;
using System.Net;


namespace RSGymPTManagement_Client.Repository
{
    public static class UserRepository
    {
        public static void CreateUsers()
        {
            using (var db = new RSGymPTManagementContext())
            {
                IList<User> users = new List<User>()
                {
                    new User{ Name = "Miguel", Code = "SujA", Password = "a123456789", Role = User.EnumRole.admin },
                    new User { Name = "Tomás", Code = "SujB",  Password = "b123456789", Role = User.EnumRole.colab },
                    new User { Name = "Helena", Code = "SujC", Password = "c123456789", Role = User.EnumRole.colab }
                };

                db.Users.AddRange(users);
                db.SaveChanges();
            }
        }
        public static void ReadUsers()
        {
            Utility.WriteTitle("User`s List");

            using (var context = new RSGymPTManagementContext())
            {
                var users = context.Users.OrderBy(u => u.Code).ToList();

                users.ForEach(u => Console.WriteLine($" ID: {u.UserID}\n Name: {u.Name}\n Code: {u.Code}\n Role: {u.Role}\n\n "));
            }


        }
        public static void CreateNewUsers()
        {
            Utility.WriteTitle("Create New User");

            Utility.WriteLineMessage(" To insert a new client, please fill in the folowing fields: \n");

            using (var context = new RSGymPTManagementContext())
            {

                var newUser = new User
                {
                    Name = Interaction.AskQuestion("Name: "),
                    Code = Validations.ValidateCode(),
                    Password = Validations.ValidatePassword(),
                    Role = Validations.ValidateRole(),
                };

                context.Users.Add(newUser);
                context.SaveChanges();


                Console.WriteLine($"\n\n New Client {newUser.Name} added with the ID: {newUser.UserID}, Code: {newUser.Code} and Role: {newUser.Role} ");
            }
        }
        public static void UpdateUserPassword()
        {

            Utility.WriteTitle("Change the User password");
            

            using (var context = new RSGymPTManagementContext())
            {
                string userCode = "";

                var codeUsers = context.Users.OrderBy(u => u.UserID).ToList();

                codeUsers.ForEach(u => Utility.WriteLineMessage($" ID: {u.UserID}\n Code: {u.Code}\n Name: {u.Name}\n"));

                Utility.WriteMessage("\n Please insert the user Code: ");
                userCode = Console.ReadLine();                                                                                      

                var user = context.Users.FirstOrDefault(u => u.Code == userCode);

                if (user != null)
                {
                    Utility.WriteLineMessage("\n Change password.\n");
                    Utility.WriteLineMessage(" Insert the new password:\n");
                    string newPassword = Console.ReadLine();
                    user.Password = newPassword;                                                                                           
                    context.SaveChanges();
                    Utility.WriteLineMessage("\n Password changed successfully\n");
                }
                else
                {
                    Console.WriteLine(" User not found.");
                }

            }




        }

    }
}
