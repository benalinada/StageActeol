using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResponse>
    {
         private readonly IUsersDBContext _usersDBContext;
        private readonly IMapper _mapper;
       public GetUserQueryHandler(IUsersDBContext usersDBContext,IMapper  mapper)
        {
            _mapper= mapper;
            _usersDBContext= usersDBContext;
        }
        public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
             User user = _usersDBContext.Users.SingleOrDefault(u=> u.Email == request.Email);
            //return  _mapper.Map<GetUserQueryResponse>(user);
            return new GetUserQueryResponse()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Function = user.Function,
                Job = user.Job
            };
            

        }
    }
}
