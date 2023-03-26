using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace Infrastructure.Persistance
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        private readonly Application.Common.Interfaces.IDateTime _dateTime;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IDateTime dateTime)
          : base(options)
        {
            _dateTime = dateTime;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<UserServer> UserServers { get; set; }
    }
}
