using AutoMapper;
using Salon.Commands.User;
using Salon.DAL.Models;
using Salon.Queries.User;
using Salon.Responses.User;

namespace Salon.Profiles.User
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, UserModel>();
            CreateMap<UserModel, UserResponse>();
            
            CreateMap<GetUsersByParamsQuery, SearchUserModel>();
            CreateMap<CreateUserCommand, UserModel>().ForMember(x=>x.Id,x=>x.Ignore());
            
            CreateMap<UpdateUserCommand, UserModel>();
            CreateMap<UserModel,UpdateUserCommand>();


        }
    }
}
