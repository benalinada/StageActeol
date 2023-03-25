using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;
using System.Diagnostics.Contracts;

namespace Application.Users.Queries.GetUser
{
    public class GetServeurQueryResponse : IMapFrom<User>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Function { get; set; }
        public string Job { get; set; }
        public  void Mapping(Profile profile)
        {
            profile.CreateMap<User,GetServeurQueryResponse>();
        }
    }
}
