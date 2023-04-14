using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D00_Utility;
using RSGymPTManagement_DAL.Model;

namespace RSGymPTManagement_Client.Configurations
{
    internal class Layout
    {
        public static void Header() 
        {
            Utility.Header("RSGym - Management System"); 
        }

        public static void Footer() 
        {

        }

        public static void SubHeader()
        {
            Session session = new Session();

            Console.ForegroundColor = ConsoleColor.Gray;
            Utility.WriteLineMessage($"{Session.Role.PadLeft(Console.WindowWidth - 10)}");
            Utility.WriteLineMessage($"{Session.Name.PadLeft(Console.WindowWidth - 10)}\n");
            Console.ResetColor();
                
            
                                                                              

        }
    }
}
