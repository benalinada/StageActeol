using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUser
{
    public class GetsUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetsUserQueryValidator() {
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
