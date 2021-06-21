using AutoMapper;
using MediatR;
using Salon.Commands.User;
using Salon.DAL.Models;
using Salon.DAL.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace Salon.Handlers.User.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response= _mapper.Map<int>(await _iUnitOfWork.User.Add(_mapper.Map<UserModel>(request)));
            _iUnitOfWork.Commit();
            return response;
        }
    }
}
