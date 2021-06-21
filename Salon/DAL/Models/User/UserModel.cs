using Salon.Common.Message;
using System.ComponentModel.DataAnnotations;

namespace Salon.DAL.Models
{
    public class UserModel : MobileNoModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = Message.M026)]
        [RegularExpression("^[a-zA-Z ]{3,25}$", ErrorMessage = Message.M027)]
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}