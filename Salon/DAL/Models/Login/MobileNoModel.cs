using Salon.Common.Message;
using System.ComponentModel.DataAnnotations;
namespace Salon.DAL.Models
{
    public class MobileNoModel
    {
        [Required(ErrorMessage = Message.M021)]
        [RegularExpression("^[0-9]{10,10}$", ErrorMessage = Message.M022)]
        public string MobileNo { get; set; }
    }
}