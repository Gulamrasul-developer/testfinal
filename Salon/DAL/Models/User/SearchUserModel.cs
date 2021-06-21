using System.ComponentModel.DataAnnotations;
namespace Salon.DAL.Models
{
    public class SearchUserModel
    {
        public string FullName{ get; set; } = "";
        [RegularExpression("^[0-9]{10,10}$", ErrorMessage = "Mobile No. must be valid")]
        public string MobileNo { get; set; } = "";
        public string Active { get; set; } = "";
    }
}
