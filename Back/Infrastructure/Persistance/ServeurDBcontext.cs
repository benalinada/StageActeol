using Application.Common.Interfaces;
using Domain.Entities;
using Ical.Net.DataTypes;
using Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
using IDateTime = Application.Common.Interfaces.IDateTime;

namespace Infrastructure.Persistance
{
    public class ServeurDBContext : DbContext, IServeurDBcontext
    {
        private readonly Application.Common.Interfaces.IDateTime _dateTime;

        public ServeurDBContext(DbContextOptions<ServeurDBContext> options, IDateTime dateTime)
          : base(options)
        {
            _dateTime = dateTime;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
          
        }

        public Microsoft.EntityFrameworkCore.DbSet<Serveur> Serveur { get; set; }
    }

   
}
