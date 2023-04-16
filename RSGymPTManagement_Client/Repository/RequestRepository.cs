using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D00_Utility;
using RSGymPTManagement_Client;
using RSGymPTManagement_Client.Configurations;
using RSGymPTManagement_DAL.Model;

namespace RSGymPTManagement_Client.Repository
{
    internal class RequestRepository
    {

        public static void CreateNewRequest()
        {
            using (var context = new RSGymPTManagementContext())
            {

                var newRequest = new Request
                {
                    ClientId = Validations.SearchClientID(),
                    PersonalTrainerId = Validations.SearchPtID(),
                    Schedule = Validations.InsertDateHour(),
                    Status = Request.EnumStatus.schedule,
                    Observation = Interaction.AskQuestion("Observations: ")
                };

                context.Requests.Add(newRequest);
                context.SaveChanges();

                Utility.WriteLineMessage($"New Request {newRequest.RequestId} scheduled at {newRequest.Schedule}, with personal trainer {newRequest.PersonalTrainer.FullName} and client {newRequest.Client.FullName}");
            }

        }

        public static void ReadRequest()
        {
            var db = new RSGymPTManagementContext();

            Utility.WriteTitle("List of Requests");

            var queryRequests = db.Requests.OrderBy(r => r.Status).ThenBy(r => r.Schedule).ToList();

            queryRequests.ForEach(r => Utility.WriteLineMessage($" - ID: {r.RequestId}\n - Client: {r.Client.FullName}\n - PT: {r.PersonalTrainer.FullName}\n - Scheduled Day and Time{r.Schedule}\n - Status: {r.Status}\n - Observations: {r.Observation}\n\n"));

        }



    }



































}
