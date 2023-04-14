using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using D00_Utility;
using RSGymPTManagement_Client.Configurations;
using RSGymPTManagement_DAL.Model;

namespace RSGymPTManagement_Client.Repository
{
    public static class PersonalTrainerRepository
    {
        public static void CreatePersonalTrainer()
        {
            using (var db = new RSGymPTManagementContext())
            {
                IList<PersonalTrainer> personalTrainers = new List<PersonalTrainer>()
                {
                   new PersonalTrainer() {PostalCodeID = 1, FirstName = "Carlos", LastName = "Borges", Code = "CBO4", NIF= "222333444", Adress = "Rua Alto das Torres", PhoneNumber = "915567890", Email = "c.ferreirinha@mail.pt"},
                   new PersonalTrainer() {PostalCodeID = 5, FirstName = "Elisa", LastName = "Ribeiro", Code = "RI15" , NIF= "234567890", Adress = "Rua 29 de junho", PhoneNumber = "934567121", Email = "elisa.pt@mail.pt"}

                };

                db.PersonalTrainers.AddRange(personalTrainers);

                db.SaveChanges();

            }
        }
        public static void ReadPersonalTrainers()
        {
            var db = new RSGymPTManagementContext();

            Utility.WriteTitle("Personal Trainers");

            var queryClients = db.PersonalTrainers.OrderBy(c => c.FirstName).ToList();

            queryClients.ForEach(p => Utility.WriteMessage($" ID:{p.PersonalTrainerID}\n Full Namw:{p.FullName}\n Code: {p.Code}\n NIF: {p.NIF}\n Full Adress: {p.FullAdress}\n Phone Number: {p.PhoneNumber}\n E-mail: {p.Email}", "", ""));

        }
        public static void CreateNewPT()
        {
            Utility.WriteTitle("Create new Personal Trainer");
            Utility.WriteLineMessage("To insert a new Personal Trainer, please fill in the folowing fields: \n");

            using (var context = new RSGymPTManagementContext())
            {


                var newPT = new PersonalTrainer
                {
                    Code = Validations.ValidateCodePT(),
                    FirstName = Interaction.AskQuestion("First Name: "),
                    LastName = Interaction.AskQuestion("Last Name: "),
                    NIF = Validations.AskNifPT("NIF: "),
                    Adress = Interaction.AskQuestion("Adress: "),
                    PostalCodeID = Validations.ExistsPostalCode(),
                    PhoneNumber = Validations.PhoneValidationPT(),
                    Email = Validations.EmailValidationPT(),

                };
          
                context.PersonalTrainers.Add(newPT);
                context.SaveChanges();

                Utility.WriteLineMessage($"New Personal Trainer {newPT.FullName} added with the ID: {newPT.PersonalTrainerID}. ");
            }

                
        }
    }
}
 