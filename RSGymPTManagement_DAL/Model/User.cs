using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RSGymPTManagement_DAL.Interfaces;

namespace RSGymPTManagement_DAL.Model
{
    public class User
    {
        #region Scalar properties
        
        
        public int UserID { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Limit of 100 characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Code must be alphanumeric")]
        [StringLength(6, MinimumLength = 4, ErrorMessage = "The code must have between 4 and 6 alphanumeric characters")]
        public string Code { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Password must be alphanumeric")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Password must have between 8 and 12 alphanumeric chatracters")]
        public string Password { get; set; }
        [Required]
        public EnumRole Role { get; set; }

        public enum EnumRole
        {
            admin = 1,
            colab = 2
        }

        #endregion

        #region Navigation properties

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<PersonalTrainer> PersonalTrainers { get; set; }

        #endregion
    }
}
