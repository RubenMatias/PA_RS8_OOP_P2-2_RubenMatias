using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using D00_Utility;
using RSGymPTManagement_DAL.Model;


namespace RSGymPTManagement_Client.Configurations
{
    public class UtilityMenu
    {
        const string headerTitle = "RSGym - Management System";
        public static Dictionary<int, string> FirstMenu()
        {
            Dictionary<int, string> initialMenu = new Dictionary<int, string>()
            {
                {1, "Login" },
                {2, "Exit" }
            };

            return initialMenu; 

        }


        public static Dictionary<int, string> AdministratorMenu()
        {
            Dictionary<int, string> secondMenu = new Dictionary<int, string>()
            {
                {1, "Users Administration" },
                {2, "Clients Administration" },
                {3, "Personal Trainers Administration" },
                {4, "Request Administration" },
                {5, "Logout" }
            };

            return secondMenu;

        }

        public static Dictionary<int, string> ColaboratorMenu()
        {
            Dictionary<int, string> thirdMenu = new Dictionary<int, string>()
            {
                {1, "Clients Administration" },
                {2, "Personal Trainers Administration" },
                {3, "Request Administration" },
                {4, "Logout" }
            };

            return thirdMenu;
        }

        public static Dictionary<int, string> UsersAdminMenu()
        {
            Dictionary<int, string> usersAdminMenu = new Dictionary<int, string>()
            {
                {1, "Create a new colaborator / administrator" },
                {2, "Change the password" },
                {3, "List all users" },
                {4, "Back to initial menu" }
            };

            return usersAdminMenu;

        }

        public static Dictionary<int, string> ClientAdminMenu()
        {
            Dictionary<int, string> clientAdmin = new Dictionary<int, string>()
             {
                {1, "Create a new Client" },
                {2, "Change the Client data" },
                {3, "List active Clients"},
                {4, "Change client status" },
                {5, "Back to initial menu"}
             };

            return clientAdmin;
        }

        public static Dictionary<int, string> PTAdminMenu()
        {
            Dictionary<int, string> ptAdminMenu = new Dictionary<int, string>()
             {
                {1, "Create a new Personal Trainer" },
                {2, "List Personal Trainer`s" },
                {3, "Back to initial menu"}
             };

            return ptAdminMenu;
        }

        public static Dictionary<int, string> RequestAdminMenu()
        {
            Dictionary<int, string> requestAdminMenu = new Dictionary<int, string>()
             {
                {1, "Create" },
                {2, "List all requests" },
                {3, "Back to initial menu"}
             };

            return requestAdminMenu;
        }

        public static void ListMenus (Dictionary<int, string> menus, string titlemenu)
        {
            Utility.WriteTitle($"{titlemenu}");
            foreach (KeyValuePair<int, string> menu in menus)
            {
                Console.WriteLine($"\t{menu.Key} - {menu.Value}\n");
            }

        }

        public static int UserChoice()
        {
            Utility.WriteMessage("\n\n\tPlease, select your option:  ");

            if (!int.TryParse(Console.ReadLine(), out int input01))
            {
                Utility.WriteLineMessage("Invalid input, please enter a number between that contains in menu!"); 
            }

            return input01;
        }

      
        public static bool SearchCorrespondence(Dictionary<int,string> menus, int choice) 
        {
            return menus.ContainsKey(choice);
        }

        public static void ChangePage()
        {
            Console.Clear();
            Utility.Header(headerTitle);
        }

        public static void Exit()
        {
            Console.Clear();
            Utility.Header("RSGym - Management System");
            Console.Write("\n\n Thank you! See you next time! \n\n Press any key to leave!");
            Console.ReadKey();
            Console.Clear();
            Environment.Exit(0);

        }
  

        public static void Logout()
        {
            Console.Clear();
            Utility.Header("RSGym - Management System");
            Console.WriteLine($"Thank you {Session.Name}, see you next time!");
            Console.WriteLine("Press any key do logout");
            Console.ReadKey();
        }



    }
}
