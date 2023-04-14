using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPTManagement_DAL.Model
{
    public class Request
    {
        #region Scalar properties
        public enum EnumStatus
        {
            schedule,
            terminated,
            canceled
        }
        [Key]
        public int RequestId { get; set; }

        public int ClientId { get; set; }

        public int PersonalTrainerId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Schedule { get; set; }

        public EnumStatus Status { get; set; }
        [MaxLength(255, ErrorMessage = "Limite de 255 caracteres")]
        public string Observation { get; set; }
        #endregion

        #region Navigation Properties

        public virtual Client Client { get; set; }
        public virtual PersonalTrainer PersonalTrainer { get; set; }

        #endregion
    }

}

