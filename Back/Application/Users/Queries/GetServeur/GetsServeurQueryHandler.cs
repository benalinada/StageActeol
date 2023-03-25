using Application.Common.Interfaces;
using Application.Users.Queries.GetUser;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetServeur
{
    public class GetsServeurQueryHandler : IRequestHandler<GetsServeurQuery, GetsServeurQueryResponse>
    {
             private readonly IServeurDBContext _serveurDBContext;
              private readonly IMapper _mapper;
    public GetsServeurQueryHandler(IUsersDBContext ServeurDBContext, IMapper mapper)
    {
        _mapper = mapper;
            _serveurDBContext = ServeurDBContext;
    }
    public async Task<GetServeurQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        Serveurs serveurs = _serveurDBContext.Serveur.SingleOrDefault(S => S.Name == request.Name);
        //return  _mapper.Map<GetUserQueryResponse>(user);
        return new GetServeurQueryResponse()
        {
            Name = Serveurs.Name,

        };


    }

        public Task<GetsServeurQueryResponse> Handle(GetsServeurQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
   
}
