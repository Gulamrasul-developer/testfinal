using MediatR;
using Salon.Responses.User;
using System.Collections.Generic;

namespace Salon.Queries.User
{
    public class GetUsersByParamsQuery : IRequest<List<UserResponse>>
    {
        public string FullName { get; set; } = "";
        public string MobileNo { get; set; } = "";
        public string Active { get; set; } = "";
    }
}
