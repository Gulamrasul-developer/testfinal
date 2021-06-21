using AutoMapper;
using MediatR;
using Salon.Commands.User;
using Salon.DAL.Models;
using Salon.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Salon.Handlers.User.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response= _mapper.Map<int>(await _iUnitOfWork.User.Update(_mapper.Map<UserModel>(request)));
            _iUnitOfWork.Commit();
            return response;
        }
    }
}