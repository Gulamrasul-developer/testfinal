using MediatR;
using Salon.Responses.User;

namespace Salon.Commands.User
{
    public class UpdateUserCommand : IRequest<int>
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
