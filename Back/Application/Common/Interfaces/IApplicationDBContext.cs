using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface  IApplicationDBContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Server> Servers { get; set; }
        DbSet<UserServer> UserServers { get; set; }

    }
}
