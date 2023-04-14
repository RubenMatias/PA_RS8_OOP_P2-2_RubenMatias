using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using RSGymPTManagement_DAL.Interfaces;

namespace RSGymPTManagement_DAL.Model
{
    public class PersonalTrainer: IPerson
    {
        #region Scalar properties

        public int PersonalTrainerID { get; set; }
        [Required]
        public int PostalCodeID { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Code must be alphanumeric")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "The code must have 4 alphanumeric characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Limit of 100 characters")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Limit of 100 characters")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [Required]
        [StringLength(9, ErrorMessage = "NIF must have 9 characters")]
        public string NIF { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Limit of 100 characters")]
        public string Adress { get; set; }
        public string FullAdress => $"{Adress} {PostalCode.PostalCodeValue} {PostalCode.Town}";
        [Required]
        [RegularExpression(@"^\+?[0-9]{3}-?[0-9]{9}$")]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"(?i)\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Invalid E-mail")]
        public string Email { get; set; }

        #endregion

        #region Navigation properties

        public virtual User Users { get; set; }
        public virtual PostalCode PostalCode { get; set; }
        public virtual ICollection<Request> Requests { get; set; }

        #endregion
    }
}
