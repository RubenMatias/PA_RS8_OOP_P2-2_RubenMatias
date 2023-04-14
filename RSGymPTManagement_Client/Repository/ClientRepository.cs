using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSGymPTManagement_DAL.Model;
using D00_Utility;
using System.Security.Cryptography;
using System.Globalization;
using System.Web;
using System.Runtime.Remoting.Services;
using RSGymPTManagement_Client.Configurations;

namespace RSGymPTManagement_Client.Repository
{
    public static class ClientRepository
    {
        public static void CreateCliente()
        {
            using (var db = new RSGymPTManagementContext())
            {

                IList<Client> client = new List<Client>()
               {
                   new Client() {PostalCodeID = 2, FirstName = "Mateus", LastName = "Santos", BirthDate = new DateTime(1982,05,23), NIF = "212765682", Adress = "Travessa do Canto", PhoneNumber = "912223334", Email = "mateus_almeida@mail.pt", Status = true},
                   new Client() {PostalCodeID = 3, FirstName = "António", LastName = "Pinto", BirthDate = new DateTime(2000,08,12), NIF = "229877654", Adress = "Rua Damião de Góis", PhoneNumber = "964567123", Email = "coelho.antonio@mail.pt", Status = true },
                   new Client() {PostalCodeID = 4, FirstName = "Rafael", LastName = "Junior", BirthDate = new DateTime(1999,12,31), NIF = "230678497", Adress = "Travessa Velha da Calçada Romada", PhoneNumber = "934890212", Email = "j.ramalho99@mail.pt", Status = true}

               };

                db.Clients.AddRange(client);

                db.SaveChanges();
            }
        }

        public static void CreateNewClient()
        {
            Utility.WriteLineMessage("To insert a new client, please fill in the folowing fields: \n");

            using (var context = new RSGymPTManagementContext())
            {                                                                                                                 

                var newClient = new Client
                {
                    FirstName = Interaction.AskQuestion("First Name: "),
                    LastName = Interaction.AskQuestion("Last Name: "),
                    BirthDate = Validations.IsValidBirthDate(),
                    NIF = Validations.AskNif("NIF: "),
                    Adress = Interaction.AskQuestion("Adress: "),
                    PostalCodeID = Validations.ExistsPostalCode(),
                    PhoneNumber = Validations.PhoneValidation(),
                    Email = Validations.EmailValidation(),
                    Observations = Interaction.AskQuestion("Observations"),
                    Status = Validations.StatusValidate(),

                };

                context.Clients.Add(newClient);
                context.SaveChanges();

                Utility.ValidationMessage($"\n New Client {newClient.FullName} added with the {newClient.ClientID}. ");
            }
        }

        public static void ReadClients()
        {
            var db = new RSGymPTManagementContext();

            Utility.WriteTitle("Clients");

            var queryClients = db.Clients.Select(c => c).Where(c => c.Status == true).OrderBy(c => c.FirstName).ToList();
            
            queryClients.ForEach(c => Utility.WriteMessage($" ID: {c.ClientID}\n Full Name: {c.FullName}\n NIF: {c.NIF}\n BirthDate: {c.BirthDate.ToShortDateString()}\n Adress: {c.FullAdress}\n PostalCode: {c.PostalCode}\n E-mail: {c.Email}\n Phone Number: {c.PhoneNumber}\n Observations: {c.Observations}\n Status: {c.StatusDescription}", "", "\n\n"));

        }

        public static void UpdateClients()
        {

            string nameContains = Interaction.AskQuestion("\"Search names that contains:");

            using (var context = new RSGymPTManagementContext())
            {

                var result = context.Clients.Where(c => c.FirstName.Contains(nameContains)).ToList();

                if (result.Count == 0)
                {
                    Utility.WriteMessage("No clients found");
                }
                else if (result.Count == 1)
                {
                    Utility.WriteLineMessage("Client found:\n");

                    var propertiesClient = new Dictionary<string, object>
                    {
                        {"ID", result[0].ClientID},
                        {"Full Name", result[0].FullName},
                        {"Date of Birth", result[0].BirthDate.ToShortDateString() },
                        {"Adress", result[0].Adress},
                        {"Phone", result[0].PhoneNumber },
                        {"E-mail", result[0].Email },
                        {"Observations", result[0].Observations }

                    };


                    Utility.WriteLineMessage("Client information:\n");

                    foreach (var property in propertiesClient)
                    {
                        Console.WriteLine($"{property.Key}: {property.Value}\n");
                    }

                    ChangeNewproperty:
                    Utility.WriteLineMessage("What do you want to change?");
                    var propertyName = Console.ReadLine();

                    Utility.WriteLineMessage($"{propertyName}: {propertiesClient.Values.ToString()}");

                    if (propertiesClient.ContainsKey(propertyName))
                    {
                        Console.WriteLine($"Enter the new value for {propertyName}:");
                        var propertyNewValue = Console.ReadLine();

                        Console.WriteLine("Do you want to save changes? (Y/N)");
                        var saveChanges = Console.ReadLine();

                        if (saveChanges == "Y")
                        {
                            result.GetType().GetProperty(propertyName)?.SetValue(result, propertyNewValue);

                            context.SaveChanges();

                            Console.WriteLine($"{propertyName} has been updated to {propertyNewValue}.");

                            Console.WriteLine("Do you want to change another property? (Y/N)");
                            var anotherProperty = Console.ReadLine();

                            if (anotherProperty == "Y")
                            {
                                goto ChangeNewproperty;
                            }
                            else if (anotherProperty == "N")
                            {
                                Console.WriteLine("The client has been uptdated!");
                            }


                        }
                        else if (saveChanges == "N")
                        {
                            Console.WriteLine("");
                        }

                    }
                    else if (result.Count > 1)
                    {
                        Console.WriteLine($"Multiple Clients found ({result.Count})");

                        foreach (var clients in result)
                        {
                            Console.WriteLine($"{clients.FullName} - {clients.BirthDate.ToShortDateString()} - {clients.Adress} - {clients.PostalCode} - {clients.PhoneNumber} - {clients.Email} - {clients.Observations}  ");
                        }

                        Console.WriteLine("Enter the client ID that you want to change data:");

                        int clientID = int.Parse(Console.ReadLine());

                        var client = result.FirstOrDefault(c => c.ClientID == clientID);

                        if (client == null)
                        {

                            Utility.ErrorMessage("Invalid client ID.");

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        Utility.ErrorMessage("Invalid property name.");
                    }

                }

            }


        }

        public static void ChangeStatus()
        {
            using (var context = new RSGymPTManagementContext())
            {
                string answer;

                do
                {
                    Utility.WriteTitle("Change Client`s Status");
                    Utility.WriteLineMessage(" What Client do you want to change STATUS? Insert ID:");

                    if (int.TryParse(Console.ReadLine(), out int clientID))
                    {

                        var client = context.Clients.Find(clientID);

                        if (client == null)
                        {

                            Utility.ErrorMessage("\n ClientID not found. Please try again! ");

                        }
                        else
                        {
                            Utility.WriteLineMessage("\n Client found!");
                            Utility.WriteLineMessage($"\n {client.FullName} is: {client.StatusDescription}");
                            
                            bool validInput = false;

                            while (!validInput)
                            {
                                Utility.WriteMessage("\n Change status (Y/N)? ");
                                var answer01 = Console.ReadLine().ToUpper();

                                switch (answer01)
                                {
                                    case "Y":                                                                                                 

                                        if (client.Status)
                                        {
                                            client.Status = false;
                                            context.SaveChanges();
                                            Console.WriteLine($"\n Client with ID: {client.ClientID} and name: {client.FullName}  has been updated to {client.StatusDescription}.");
                                            validInput = true;
                                        }
                                        else
                                        {
                                            client.Status = true;
                                            context.SaveChanges();
                                            Console.WriteLine($"\n Client with ID: {client.ClientID} and name: {client.FullName}  has been updated to {client.StatusDescription}.");
                                            validInput = true;
                                        }
                                        break;
                                    case "N":
                                        validInput = true;
                                        break;
                                }

                            }
 
                        }

                    }
                    else
                    {
                        Utility.ErrorMessage("Invalid input, please try again!");
                    }

                    Utility.WriteMessage("\n\n Want to search for another client to change the status (Y/N)? ");
                    answer = Console.ReadLine().ToUpper();

                } while (answer == "Y");

            } 

        }

        public static string SearchClientName(string question)
        {
            Utility.WriteLineMessage(question);
            string answer = Console.ReadLine();

            return answer;
        }

        public static void SearchClientID()
        {
            bool continueSearching = true;

            while (continueSearching)
            {
                Utility.WriteLineMessage("What Client do you want to change STATUS? Insert ID:");

                if (int.TryParse(Console.ReadLine(), out int clientID))
                {

                    using (var context = new RSGymPTManagementContext())
                    {

                        var client = context.Clients.Find(clientID);

                        if (client != null)
                        {
                            Console.WriteLine("Client found!");
                            Console.WriteLine($"The client status is: {client.Status}");
                            Console.WriteLine("Change status? (Y/N)");
                            var answer = Console.ReadLine();

                            switch (answer)
                            {
                                case "Y":
                                    if (client.Status)
                                    {
                                        client.Status = false;
                                        context.SaveChanges();
                                    }
                                    else
                                    {
                                        client.Status = true;
                                        context.SaveChanges();
                                    }
                                    break;
                                case "N":
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Client with ID {clientID} not found, try again!");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again!");
                }

                Console.WriteLine("Search for another clientID? (Y/N)");
                string choice = Console.ReadLine();

                if (!string.IsNullOrEmpty(choice) && choice.ToLower() == "n")
                {
                    continueSearching = false;
                }
            }

        }


    }


}
