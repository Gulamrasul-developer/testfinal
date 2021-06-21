using Salon.Common.Message;
using System.ComponentModel.DataAnnotations;
namespace Salon.DAL.Models
{
    public class UserGroupModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = Message.M026)]
        [RegularExpression("^[a-zA-Z ]{3,25}$", ErrorMessage = Message.M027)]
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
