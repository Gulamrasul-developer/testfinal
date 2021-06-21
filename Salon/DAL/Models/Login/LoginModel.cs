using Salon.Common.Message;
using System.ComponentModel.DataAnnotations;
namespace Salon.DAL.Models
{
    public class LoginModel : MobileNoModel
    {
        [Required(ErrorMessage = Message.M024)]
        [MinLength(8, ErrorMessage = Message.M025)]
        [MaxLength(8, ErrorMessage = Message.M025)]
        public string Password { get; set; }
    }
}
