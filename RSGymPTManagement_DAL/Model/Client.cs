using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RSGymPTManagement_DAL.Interfaces;

namespace RSGymPTManagement_DAL.Model
{
    public class Client: IPerson
    {
        #region Scalar properties

        public int ClientID { get; set; }
        [Required]
        public int PostalCodeID { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Limit of 100 characters")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Limit of 100 characters")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
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
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?i)\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        public string Observations { get; set; }
        [Required]
        public Boolean Status { get; set; }

        public string StatusDescription 
        {
            get { return Status ? "Inactive" : "Active" ;  } 
            set { Status = (value.ToLower() == "Inactive"); } 
        }

        #endregion

        #region Navigation properties

        public virtual User Users { get; set; }
        public virtual PostalCode PostalCode { get; set; }

        public virtual ICollection<Request> Requests { get; set; }


        #endregion
    }
}
