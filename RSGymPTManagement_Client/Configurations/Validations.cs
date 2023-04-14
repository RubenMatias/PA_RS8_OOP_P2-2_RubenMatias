using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using D00_Utility;
using RSGymPTManagement_DAL.Model;

namespace RSGymPTManagement_Client.Configurations
{
    public class Validations
    {
        public static bool IsValidCode(string code)
        {
            return string.IsNullOrEmpty(code) || Regex.IsMatch(code, @"^[a-zA-Z0-9]{4,6}$");
        }

        public static bool IsValidPassword(string password)
        {
            return string.IsNullOrEmpty(password) || Regex.IsMatch(password, @"^[a-zA-Z0-9]{8,12}$");
        }

        /*
        O NIF tem 9 dígitos, sendo o último o digito de controlo. Para ser calculado o digito de controlo:

        Multiplique o 8.º dígito por 2, o 7.º dígito por 3, o 6.º dígito por 4, o 5.º dígito por 5, o 4.º dígito por 6, o 3.º dígito por 7, o 2.º dígito por 8 e o 1.º dígito por 9;
        Some os resultados;
        Calcule o resto da divisão do número por 11;
        Se o resto for 0 (zero) ou 1 (um) o dígito de controlo será 0 (zero);
        Se for outro qualquer algarismo X, o dígito de controlo será o resultado da subtracção 11 - X.
        */


        public static bool IsValidNIF(string nif)
        {

            if (nif.Length != 9)
                return false;

            if (!int.TryParse(nif, out int nifval))
                return false;

            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
                sum += (nif[i] - '0') * (9 - i);
            }

            int checkDigit = 11 - (sum % 11);
            if (checkDigit >= 10)
                checkDigit = 0;

            return (checkDigit == (nif[8]) - '0');

        }

        public static string AskNif(string newNif)
        {
            using (var context = new RSGymPTManagementContext())
            {
                newNif = "";

                while (true)
                {
                    Utility.WriteMessage("NIF: ", " - ");
                    newNif = Console.ReadLine();

                    var nifExists = context.Clients.Any(nif => nif.NIF == newNif);

                    if (!IsValidNIF(newNif))
                    {
                        Utility.ErrorMessage("Invalid NIF! Try Again!");
                    }
                    else if (nifExists)
                    {
                        Utility.ErrorMessage("Nif already exists! Try Again!");
                    }
                    else
                    {
                        break;
                    }



                }

                return newNif;


            }

        }

        public static string AskNifPT(string newNif)
        {
            using (var context = new RSGymPTManagementContext())
            {
                newNif = "";

                while (true)
                {
                    Utility.WriteMessage("NIF: ", " - ");
                    newNif = Console.ReadLine();

                    var nifExists = context.PersonalTrainers.Any(nif => nif.NIF == newNif);

                    if (!IsValidNIF(newNif))
                    {
                        Utility.ErrorMessage("Invalid NIF! Try Again!");
                    }
                    else if (nifExists)
                    {
                        Utility.ErrorMessage("Nif already exists! Try Again!");
                    }
                    else
                    {
                        break;
                    }



                }

                return newNif;


            }

        }

        public static bool IsValidPhone(string phone)
        {
            Regex regex = new Regex(@"^\+?[0-9]{3}-?[0-9]{9}$");
            return regex.IsMatch(phone);

        }

        public static string PhoneValidation() 
        {
            using (var context = new RSGymPTManagementContext())
            {

                string newPhone = "";
                

                while (true)
                {
                newPhone:
                    Console.Write("Phone number (+XXX-XXXXXXXXX): ", " - ");
                    newPhone = Console.ReadLine();

                    var phoneExists = context.Clients.Any(u => u.PhoneNumber == newPhone);

                    if (!IsValidPhone(newPhone))
                    {
                        Utility.ErrorMessage("Invalid Phone number! Try Again!");
                    }
                    else if (phoneExists)
                    {
                        Utility.ErrorMessage("Phone number already exists!");
                        string answer = "";

                    phoneInput:

                        while (answer != "S" || answer != "N")
                        {
                            Utility.WriteMessage("Want to save (S), or insert new phone number (N): ");
                            answer = Console.ReadLine().ToUpper();

                            if (answer != "S" || answer != "N")
                            {
                                Utility.ErrorMessage("Wrong input");
                                goto phoneInput;
                            }
                        }

                        if (answer == "S")
                        {
                            break;
                        }
                        else if (answer == "N")
                        {
                            goto newPhone;
                        }
                    }
                    else
                    {
                        break;
                    }

                }

                return newPhone;

            }

        }

        public static string PhoneValidationPT()
        {
            using (var context = new RSGymPTManagementContext())
            {

                string newPhone = "";


                while (true)
                {
                newPhone:
                    Console.Write("Phone number (+XXX-XXXXXXXXX): ", " - ");
                    newPhone = Console.ReadLine();

                    var phoneExists = context.PersonalTrainers.Any(nif => nif.PhoneNumber == newPhone);

                    if (!IsValidPhone(newPhone))
                    {
                        Utility.ErrorMessage("Invalid Phone number! Try Again!");
                    }
                    else if (phoneExists)
                    {
                        Utility.ErrorMessage("Phone number already exists!");
                        string answer = "";

                    phoneInput:

                        while (answer != "S" || answer != "N")
                        {
                            Utility.WriteMessage("Want to save (S), or insert new phone number (N): ");
                            answer = Console.ReadLine().ToUpper();

                            if (answer != "S" || answer != "N")
                            {
                                Utility.ErrorMessage("Wrong input");
                                goto phoneInput;
                            }
                        }

                        if (answer == "S")
                        {
                            break;
                        }
                        else if (answer == "N")
                        {
                            goto newPhone;
                        }
                    }
                    else
                    {
                        break;
                    }

                }

                return newPhone;

            }

        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                string pattern = @"(?i)\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
                return Regex.IsMatch(email, pattern);
            }
            catch (RegexMatchTimeoutException)
            {

                return false;

            }


        }

        public static string EmailValidation() 
        {
            using (var context = new RSGymPTManagementContext())
            {
                string newEmail = "";

                while (true)
                {
                newEmail:

                    Console.Write("E-mail: ", " - ");
                    newEmail = Console.ReadLine();

                    var emailExists = context.Clients.Any(c => c.Email == newEmail);


                    if (!Validations.IsValidEmail(newEmail))
                    {
                        Utility.ErrorMessage("Invalid E-mail! Try Again!");
                    }
                    else if (emailExists)
                    {
                        Utility.ErrorMessage("E-mail already exists!");
                    Emailinput:
                        Utility.WriteMessage("Want to save, ou insert other E-mail (S/I): ");
                        var answer = Console.ReadLine().ToUpper();

                        if (answer == "S")
                        {
                            break;
                        }
                        else if (answer == "I")
                        {
                            goto newEmail;
                        }
                        else
                        {
                            Utility.ErrorMessage("Wrong input");
                            goto Emailinput;
                        }

                    }
                    else
                    {
                        break;
                    }
                }

                return newEmail;

            }

        }

        public static string EmailValidationPT()
        {
            using (var context = new RSGymPTManagementContext())
            {
                string newEmail = "";

                while (true)
                {
                newEmail:

                    Console.Write("E-mail: ", " - ");
                    newEmail = Console.ReadLine();

                    var emailExists = context.PersonalTrainers.Any(nif => nif.Email == newEmail);


                    if (!Validations.IsValidEmail(newEmail))
                    {
                        Utility.ErrorMessage("Invalid E-mail! Try Again!");
                    }
                    else if (emailExists)
                    {
                        Utility.ErrorMessage("E-mail already exists!");
                    Emailinput:
                        Utility.WriteMessage("Want to save, ou insert other E-mail (S/I): ");
                        var answer = Console.ReadLine().ToUpper();

                        if (answer == "S")
                        {
                            break;
                        }
                        else if (answer == "I")
                        {
                            goto newEmail;
                        }
                        else
                        {
                            Utility.ErrorMessage("Wrong input");
                            goto Emailinput;
                        }

                    }
                    else
                    {
                        break;
                    }
                }

                return newEmail;

            }

        }

        public static bool IsValidDate(DateTime date)
        {
            return DateTime.TryParse(Console.ReadLine(), out date) || date.Date <= DateTime.Today;
        }

        public static DateTime InsertDateHour() 
        {

            DateTime InsertDate() 
            {
                DateTime date;

                do
                {
                    Console.Write("Data (yyyy/mm/dd): ");

                } while (!DateTime.TryParse(Console.ReadLine(), out date) || date.Date <= DateTime.Today);


                return date;

            }
            
            TimeSpan InsertHour() 
            {

                TimeSpan hour;
                do
                {

                    Console.Write("Hour (hh:mm): ");

                } while (!TimeSpan.TryParse(Console.ReadLine(), out hour));

                return hour;

            }

            DateTime newSchedule = InsertDate() + InsertHour();

            return newSchedule;
        } 

     

        public static DateTime IsValidBirthDate()
        {
            Utility.WriteMessage("Birthdate (dd/mm/yyyy): ", " - ");
            string date = Console.ReadLine();

            DateTime dateOfBirth = DateTime.MinValue;
            
            while (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth)) 
            {
                Console.WriteLine("Invalid date format, please enter the date of birth in dd/mm/yyyy format: ");
            }

            return dateOfBirth;

        }

        public static int ExistsPostalCode() 
        {
            using (var context = new RSGymPTManagementContext())
            {

                Utility.WriteLineMessage("\n Check if the PostalCode exists!","","\n");

                Utility.WriteMessage("Postal Code (XXXX-XXX): ", " - ","");
                var postalCodeinput = Console.ReadLine();

                Utility.WriteMessage("Town: ", " - ");
                var town = Console.ReadLine();

                while (true)
                {
                    var postalCode = context.PostalCodes.Where(p => p.PostalCodeValue == postalCodeinput && p.Town == town).FirstOrDefault();

                    if (postalCode == null)
                    {
                        postalCode = new PostalCode { PostalCodeValue = postalCodeinput, Town = town };
                        context.PostalCodes.Add(postalCode);
                        context.SaveChanges();

                        Utility.WriteLineMessage($"\n\tNew Postal Code {postalCodeinput} added with the {postalCode.PostalCodeID}. ","","\n");
                        
                    }
                    else
                    {
                        Console.WriteLine($"\n\tPostal Code {postalCodeinput} already exists, with the {postalCode.PostalCodeID}. ");
                        
                    }

                    return postalCode.PostalCodeID;
                }

            }

        }

        public static bool StatusValidate() 
        {
            Utility.WriteMessage("Status ( True - active / False - inactive ): ", " - ","\n");
            bool status;

            while (!bool.TryParse(Console.ReadLine(), out status))
            {
                Utility.ErrorMessage("Invalid status. Please enter the client`s status as either 'true' for active or 'false' for inactive: ");
            }

            return status;

        }

        public static string ValidateCode() 
        {
            using (var db = new RSGymPTManagementContext())
            {

                string newCode = "";

                while (true)
                {
                    Utility.WriteMessage("Code (Alphanumeric, 4 to 6 characters): ", " - ");
                    newCode = Console.ReadLine();

                    var codeExists = db.Users.Any(u => u.Code == newCode);

                    if (!Validations.IsValidCode(newCode))
                    {

                        Utility.ErrorMessage("Invalid code! Try Again!");

                    }
                    else if (codeExists)
                    {
                        Utility.ErrorMessage("Code already exists! Try Again!");
                    }
                    else
                    {
                        break;
                    }
                }
                return newCode;
            }

        
        }

        public static string ValidateCodePT()
        {
            using (var db = new RSGymPTManagementContext())
            {

                string newCode = "";

                while (true)
                {
                    Utility.WriteMessage("Code (Alphanumeric, 4 characters): ", " - ");
                    newCode = Console.ReadLine();

                    var codeExists = db.PersonalTrainers.Any(p => p.Code == newCode);

                    if (!Validations.IsValidCode(newCode))
                    {

                        Utility.ErrorMessage("Invalid code! Try Again!");

                    }
                    else if (codeExists)
                    {
                        Utility.ErrorMessage("Code already exists! Try Again!");
                    }
                    else
                    {
                        break;
                    }
                }
                return newCode;
            }
        }

        public static int SearchClientID() 
        {
            using (var context = new RSGymPTManagementContext())
            {
                Console.WriteLine("To insert a new request, please fill in the folowing fields: ");


                int clientID = 0;
                bool clientIDValid = false;

                do
                {
                    Utility.WriteLineMessage("Client ID:");
                    clientIDValid = (int.TryParse(Console.ReadLine(), out clientID));

                    if (!clientIDValid)
                    {
                        Utility.ErrorMessage("Invalid input, try again!");
                    }
                    else
                    {
                        var client = context.Clients.Find(clientID);

                        if (client == null)
                        {
                            Utility.WriteLineMessage($"Client with ID {clientID} not found, try again!");
                            clientIDValid = false;
                        }
                        else
                        {
                            Utility.WriteLineMessage($"{clientID} - ok!");
                            clientIDValid = true;
                        }


                    }

                } while (!clientIDValid);

                return clientID;

            }

        }

        public static int SearchPtID()
        {
            using (var context = new RSGymPTManagementContext())
            {

                int ptID = 0;
                bool ptIDValid = false;

                do
                {
                    Utility.WriteLineMessage("Personal Trainer ID:");
                    ptIDValid = (int.TryParse(Console.ReadLine(), out ptID));

                    if (!ptIDValid)
                    {
                        Utility.ErrorMessage("Invalid input, try again!");
                    }
                    else
                    {
                        var pt = context.PersonalTrainers.Find(ptID);

                        if (pt == null)
                        {
                            Console.WriteLine($"Personal Trainer with ID: {ptID}, not found, try again!");
                            ptIDValid = false;
                        }
                        else
                        {
                            Utility.WriteLineMessage($"{ptID} - ok!");
                            ptIDValid = true;
                        }

                    }

                } while (!ptIDValid);

                return ptID;
            }
            

        }

        public static string ValidatePassword() 
        {
            string newPassword = "";

            while (true)
            {
                Utility.WriteMessage("Password (Alphanumeric, 8 to 12 characters): ", " - ");
                newPassword = Console.ReadLine();

                if (!Validations.IsValidPassword(newPassword))
                {
                    Utility.ErrorMessage("Invalid password! Try Again!");
                }
                else
                {
                    break;
                }
            }
            return newPassword;
        }

        public static User.EnumRole ValidateRole() 
        {
            User.EnumRole enumRole;

            while (true)
            {
                Utility.WriteMessage("Role (admin / colab): ", " - ", "");

                string inputRole = Console.ReadLine();

                if (Enum.TryParse(inputRole, out enumRole))
                {
                    Utility.ValidationMessage("\n Role Validated!");
                    break;
                }
                else
                {
                    Utility.ErrorMessage("Invalid role insert, please insert one of the two options!");
                    break;
                }

            }

            return enumRole;

        }


    }



}
