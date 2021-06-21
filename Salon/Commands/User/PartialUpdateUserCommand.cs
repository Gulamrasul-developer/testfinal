using MediatR;

namespace Salon.Commands.User
{
    public class PartialUpdateUserCommand : IRequest<int>
    {
        public long Id { get; }
        public PartialUpdateUserCommand(long id)
        {
            Id = id;
        }
    }
}
