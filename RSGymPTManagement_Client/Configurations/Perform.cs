using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using D00_Utility;
using RSGymPTManagement_Client.Repository;
using RSGymPTManagement_DAL.Model;

namespace RSGymPTManagement_Client.Configurations
{
    public class Perform
    {
        const string headerTitle = "RSGym - Management System";


        public static void MenuLoginExit()
        {
            bool validInput = false;

            while (!validInput)
            {
                UtilityMenu.ChangePage();
                UtilityMenu.ListMenus(UtilityMenu.FirstMenu(), "Initial Menu");

                int input01 = UtilityMenu.UserChoice();

                if (UtilityMenu.SearchCorrespondence(UtilityMenu.FirstMenu(), input01))
                {
                    switch (input01)
                    {
                        case 1:
                            UtilityMenu.ChangePage();
                            AcessLogin(Login());
                            break;
                        case 2:
                            UtilityMenu.Exit();
                            break;
                        default:
                            break;
                    }
                    validInput = true;
                }
                else
                {
                    Utility.ErrorMessage("Wrong input");
                }

            }
        }

        public static (string, string) Login()
        {
            // Guarda os dados do login numa nova variável do tipo tuple
            

            (string, string) userResult = DataLogin.LoopLogin(DataLogin.MessageLogin(DataLogin.ValidateCredentials(DataLogin.ReadCredentials())));

            return userResult;

        }


        public static void AcessLogin((string, string) credentials)
        {
            switch (credentials.Item1)
            {
                case "admin":
                    MenuAdministrator();
                    break;
                case "colab":
                    MenuColaborator();
                    break;
            }
        }

        public static void MenuAdministrator()
        {

            bool validInput = false;

            while (!validInput)
            {
                UtilityMenu.ChangePage();
                Layout.SubHeader();
                UtilityMenu.ListMenus(UtilityMenu.AdministratorMenu(), "Administrator Menu");
                int input02 = UtilityMenu.UserChoice();

                if (UtilityMenu.SearchCorrespondence(UtilityMenu.AdministratorMenu(), input02))
                {
                    switch (input02)
                    {
                        case 1: // Users administration 
                            UsersAdministrationMenu();
                            break;
                        case 2: // Clients administration
                            ClientAdministrationMenu();
                            break;
                        case 3:   // Personal Trainers administration
                            PtAdministrationMenu();
                            break;
                        case 4:   // Request administration
                            RequestAdministrationMenu();
                            break;
                        case 5:   // Logout and back to login menu
                            UtilityMenu.Logout();
                            MenuLoginExit();
                            break;
                    }
                    validInput = true;
                }
                else
                {
                    Utility.ErrorMessage("Wrong input");
                }

            }
        }

        public static void MenuColaborator()
        {
            bool validInput = false;

            while (!validInput)
            {
                UtilityMenu.ChangePage();
                Layout.SubHeader();
                UtilityMenu.ListMenus(UtilityMenu.ColaboratorMenu(), "Colaborator Menu");

                int input03 = UtilityMenu.UserChoice();

                if (UtilityMenu.SearchCorrespondence(UtilityMenu.ColaboratorMenu(), input03))
                {
                    switch (input03)
                    {
                        case 1: // Client`s administration
                            ClientAdministrationMenu();
                            break;
                        case 2:  // Personal Trainers administration
                            PtAdministrationMenu();
                            break;
                        case 3:  // Request  administration
                            RequestAdministrationMenu();
                            break;
                        case 4: // Logout
                            UtilityMenu.Logout();
                            MenuLoginExit();
                            break;
                    }
                    validInput = true;
                }
                else
                {
                    Utility.ErrorMessage("Wrong input");
                }



            }
        }


        public static void UsersAdministrationMenu()
        {
            bool validInput = false;

            while (!validInput)
            {
            UsersAdministrationMenu:
                UtilityMenu.ChangePage();
                Layout.SubHeader();
                UtilityMenu.ListMenus(UtilityMenu.UsersAdminMenu(), "User`s Administration Menu");

                int input04 = UtilityMenu.UserChoice();

                if (UtilityMenu.SearchCorrespondence(UtilityMenu.UsersAdminMenu(), input04))
                {
                    switch (input04)
                    {
                        case 1: // Create a new user 
                            UtilityMenu.ChangePage();
                            Layout.SubHeader();
                            UserRepository.CreateNewUsers();
                            Utility.WriteMessage("\n\nPress any key to return to menu");
                            Console.ReadKey();
                            goto UsersAdministrationMenu;

                        case 2: // Change the user password
                            UtilityMenu.ChangePage();
                            Layout.SubHeader();
                            UserRepository.UpdateUserPassword();
                            Utility.WriteMessage("\n\nPress any key to return to menu");
                            Console.ReadKey();
                            goto UsersAdministrationMenu;

                        case 3:   // List all users
                            UtilityMenu.ChangePage();
                            Layout.SubHeader();
                            UserRepository.ReadUsers();
                            Utility.WriteMessage("\n\nPress any key to return to menu");
                            Console.ReadKey();
                            goto UsersAdministrationMenu;

                        case 4:   // back to initial menu
                            MenuAdministrator();
                            break;
                    }
                    validInput = true;
                }
                else
                {
                    Utility.ErrorMessage("Wrong input");
                }

            }

        }

        public static void ClientAdministrationMenu()
        {
            bool validInput = false;
        ClientAdministrationMenu:
            while (!validInput)
            {
                UtilityMenu.ChangePage();
                Layout.SubHeader();
                UtilityMenu.ListMenus(UtilityMenu.ClientAdminMenu(), "Client`s Administration Menu");
                int input05 = UtilityMenu.UserChoice();

                if (UtilityMenu.SearchCorrespondence(UtilityMenu.ClientAdminMenu(), input05))
                {
                    switch (input05)
                    {
                        case 1: // Create a new Client
                            UtilityMenu.ChangePage();
                            Layout.SubHeader();
                            ClientRepository.CreateNewClient();
                            Utility.WriteMessage("\n\nPress any key to return to menu");
                            Console.ReadKey();
                            goto ClientAdministrationMenu;
                        case 2: // Change the data of client
                            UtilityMenu.ChangePage();
                            Layout.SubHeader();
                            ClientRepository.UpdateClients();
                            Utility.WriteMessage("\n\nPress any key to return to menu");
                            Console.ReadKey();
                            goto ClientAdministrationMenu;
                        case 3:   // List active clients
                            UtilityMenu.ChangePage();
                            Layout.SubHeader();
                            ClientRepository.ReadClients();
                            Utility.WriteMessage("\n\nPress any key to return to menu");
                            Console.ReadKey();
                            goto ClientAdministrationMenu;
                        case 4:   // change status
                            UtilityMenu.ChangePage();
                            Layout.SubHeader();
                            ClientRepository.ChangeStatus();
                            Utility.WriteMessage("\n\nPress any key to return to menu");
                            Console.ReadKey();
                            goto ClientAdministrationMenu;
                        case 5:  // back to initial menu
                            if (Session.Role == "admin")
                            {
                                MenuAdministrator();
                            }
                            else
                            {
                                MenuColaborator();
                            }
                            break;
                    }
                    validInput = true;
                }
                else
                {
                    Utility.ErrorMessage("Wrong input");
                }
            }

        }

        public static void PtAdministrationMenu()
        {
            bool validInput = false;

        PtAdministrationMenu:
            while (!validInput)
            {
                UtilityMenu.ChangePage();
                Layout.SubHeader();
                UtilityMenu.ListMenus(UtilityMenu.PTAdminMenu(), "PT`s Administration Menu");
                int input05 = UtilityMenu.UserChoice();

                if (UtilityMenu.SearchCorrespondence(UtilityMenu.PTAdminMenu(), input05))
                {
                    switch (input05)
                    {
                        case 1: // Create a new PT
                            UtilityMenu.ChangePage();
                            Layout.SubHeader();
                            PersonalTrainerRepository.CreateNewPT();
                            Utility.WriteMessage("\n\nPress any key to return to menu");
                            Console.ReadKey();
                            goto PtAdministrationMenu;
                        case 2: // Order by name
                            UtilityMenu.ChangePage();
                            Layout.SubHeader();
                            PersonalTrainerRepository.ReadPersonalTrainers();
                            Utility.WriteMessage("\n\nPress any key to return to menu");
                            Console.ReadKey();
                            goto PtAdministrationMenu;
                        case 3:  // back to initial menu
                            if (Session.Role == "admin")
                            {
                                MenuAdministrator();
                            }
                            else
                            {
                                MenuColaborator();
                            }
                            break;
                    }
                    validInput = true;
                }
                else
                {
                    Utility.ErrorMessage("Wrong input");
                }

            }

        }

        public static void RequestAdministrationMenu()
        {
            bool validInput = false;
            RequestAdministrationMenu:
            while (!validInput)
            {
                UtilityMenu.ChangePage();
                Layout.SubHeader();
                UtilityMenu.ListMenus(UtilityMenu.RequestAdminMenu(), "Request Administration Menu");
                int input06 = UtilityMenu.UserChoice();

                if (UtilityMenu.SearchCorrespondence(UtilityMenu.RequestAdminMenu(), input06))
                {
                    switch (input06)
                    {
                        case 1: // Create a new pt request
                            UtilityMenu.ChangePage();
                            Layout.SubHeader();
                            RequestRepository.CreateNewRequest();
                            Utility.WriteMessage("\n\nPress any key to return to menu");
                            Console.ReadKey();
                            goto RequestAdministrationMenu;
                        case 2: // List and Order by status, date and hour
                            UtilityMenu.ChangePage();
                            Layout.SubHeader();
                            RequestRepository.ReadRequest();
                            Utility.WriteMessage("\n\nPress any key to return to menu");
                            Console.ReadKey();
                            goto RequestAdministrationMenu;
                        case 3:  // back to initial menu
                            if (Session.Role == "admin")
                            {
                                MenuAdministrator();
                            }
                            else
                            {
                                MenuColaborator();
                            }
                            break;

                    }
                    validInput = true;
                }
                else
                {
                    Utility.ErrorMessage("Wrong input");
                }

            }

        }
    }



}



