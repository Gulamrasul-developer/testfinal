using AutoMapper;
using MediatR;
using Salon.Commands.User;
using Salon.DAL.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace Salon.Handlers.User.Commands
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;
        public DeleteUserHandler(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response= _mapper.Map<int>(await _iUnitOfWork.User.Delete(request.Id));
            _iUnitOfWork.Commit();
            return response;
        }
    }
}
