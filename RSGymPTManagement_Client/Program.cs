using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D00_Utility;
using RSGymPTManagement_Client.Configurations;
using RSGymPTManagement_Client.Repository;
using RSGymPTManagement_DAL;
using RSGymPTManagement_DAL.Model;

namespace RSGymPTManagement_Client
{
    internal class Program
    {
        const string headerTitle = "RSGym - Management System";
        static void Main(string[] args)
        {
            try
            {
                Utility.SetUnicodeConsole();

                Perform.StartConsoleApp();
                Perform.MenuLoginExit();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }

                Utility.TerminateConsole();

            }

        }



    }
}
