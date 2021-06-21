using AutoMapper;
using MediatR;
using Salon.DAL.UnitOfWork;
using Salon.Queries.User;
using Salon.Responses.User;
using System.Threading;
using System.Threading.Tasks;

namespace Salon.Handlers.User.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _mapper = mapper;
        }
        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserResponse>(await _iUnitOfWork.User.GetById(request.Id));
        }
    }
}
