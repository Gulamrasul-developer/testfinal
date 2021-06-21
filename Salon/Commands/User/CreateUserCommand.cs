using MediatR;
using Salon.Common.Message;
using Salon.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Salon.Commands.User
{
    public class CreateUserCommand:MobileNoModel,IRequest<int>
    {
 
        [Required(ErrorMessage = Message.M026)]
        [RegularExpression("^[a-zA-Z ]{3,25}$", ErrorMessage = Message.M027)]
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
