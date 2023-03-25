using Application.Users.Queries.GetUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetServeur
{
    internal class GetsServeurQueryValidator: AbstractValidator<GetUserQuery>
    {
        public GetsServeurQueryValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
