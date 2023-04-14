using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using D00_Utility;
using RSGymPTManagement_DAL.Model;

namespace RSGymPTManagement_Client.Configurations
{
    public class Interaction
    {
        public static string AskQuestion(string text) 
        {
            Utility.WriteMessage(text, " - ");
            string readanswer = Console.ReadLine();

            return readanswer;
        }

 

        
        
        
        
        

        
        
        
        

        

        
        
        
        
        
        
        
        
        
        
        
        



        

        


        

        
    }
}
