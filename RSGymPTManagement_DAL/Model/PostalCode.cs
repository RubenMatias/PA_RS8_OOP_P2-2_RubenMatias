using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPTManagement_DAL.Model
{
    public class PostalCode
    {
        #region Scalar Properties

        public int PostalCodeID { get; set; }
        [Required]
        [RegularExpression(@"^\d{4}-\d{3}$")]
        public string PostalCodeValue { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Limite de 100 caracteres")]
        public string Town{ get; set; }

        #endregion

        #region Navigation properties

        public virtual ICollection<PersonalTrainer> PersonalTrainers { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        #endregion
    }
}
