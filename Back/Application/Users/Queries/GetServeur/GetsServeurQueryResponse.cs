using Application.Common.Mapping;
using Application.Users.Queries.GetUser;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetServeur
{
    internal class GetsServeurQueryResponse : IMapFrom<Serveurs>
    {
        public string Name { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Serveurs, GetServeurQueryResponse>();
    }
    
    }
}
