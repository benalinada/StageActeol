using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    internal interface IServeurDBContext
    {
        DbSet<Serveurs> Serveur{ get; set; }
    }
}
