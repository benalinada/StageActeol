using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class UsersDBContext : DbContext, IUsersDBContext
    {
        private readonly Application.Common.Interfaces.IDateTime _dateTime;

        public UsersDBContext(DbContextOptions<UsersDBContext> options, IDateTime dateTime)
          : base(options)
        {
            _dateTime = dateTime;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<User> Users { get; set; }
    }
}
