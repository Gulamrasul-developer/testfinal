using AutoMapper;
using MediatR;
using Salon.DAL.Models;
using Salon.DAL.UnitOfWork;
using Salon.Queries.User;
using Salon.Responses.User;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Salon.Handlers.User.Queries
{
    public class GetUsersByParamsQueryHandler : IRequestHandler<GetUsersByParamsQuery, List<UserResponse>>
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;

        public GetUsersByParamsQueryHandler(IMapper mapper, IUnitOfWork iUnitOfWork)
        {
            _mapper = mapper;
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<List<UserResponse>> Handle(GetUsersByParamsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<UserResponse>>(await _iUnitOfWork.User.Search(_mapper.Map<SearchUserModel>(request)));
        }
    }
}
