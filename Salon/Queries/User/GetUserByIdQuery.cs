using MediatR;
using Salon.Responses.User;

namespace Salon.Queries.User
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public long Id { get; set; }
        public GetUserByIdQuery(long id)
        {
            Id = id;
        }
    }
}
