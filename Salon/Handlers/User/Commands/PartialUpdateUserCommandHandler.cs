using AutoMapper;
using MediatR;
using Salon.Commands.User;
using Salon.DAL.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace Salon.Handlers.User.Commands
{
    public class PartialUpdateUserCommandHandler : IRequestHandler<PartialUpdateUserCommand, int>
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;
        public PartialUpdateUserCommandHandler(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(PartialUpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response= _mapper.Map<int>(await _iUnitOfWork.User.Active(request.Id));
            _iUnitOfWork.Commit();
            return response;
        }
    }
}
