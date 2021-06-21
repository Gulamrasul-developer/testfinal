using MediatR;

namespace Salon.Commands.User
{
    public class DeleteUserCommand : IRequest<int>
    {
        public long Id { get; }
        public DeleteUserCommand(long id)
        {
            Id = id;
        }
    }
}