using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D00_Utility;
using RSGymPTManagement_DAL.Model;


namespace RSGymPTManagement_Client.Repository
{
    public static class PostalCodeRepository
    {
        public static void CreateCP()
        {

            using (var db = new RSGymPTManagementContext())
            {
                IList<PostalCode> postalCodes = new List<PostalCode>()
                {
                    new PostalCode {PostalCodeValue = "4430-010", Town = "Vila Nova de Gaia"},
                    new PostalCode {PostalCodeValue = "4415-100", Town = "Grijó"},
                    new PostalCode {PostalCodeValue = "4420-100", Town = "Gondomar"},
                    new PostalCode {PostalCodeValue = "4410-100", Town = "São Felix da Marinha"},
                    new PostalCode {PostalCodeValue = "4400-020", Town = " Vila Nova de Gaia"}

                };

                db.PostalCodes.AddRange(postalCodes);

                db.SaveChanges();


            }
        }
    }
}
