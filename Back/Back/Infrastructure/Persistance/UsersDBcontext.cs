using Application.Common.Interfaces;
using Domain.Entities;
using Ical.Net.DataTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{
    public class UsersDBContext : DbContext, IUsersDBContext
    {
        private readonly IDateTime _dateTime;

        public UsersDBContext(DbContextOptions<UsersDBContext> options, IDateTime dateTime)
          : base(options)
        {
            _dateTime = dateTime;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<User> Users { get; set; }
    }
}
