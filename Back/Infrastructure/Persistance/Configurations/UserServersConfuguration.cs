using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{
    public class UserServersConfuguration : IEntityTypeConfiguration<UserServer>
    {
        public void Configure(EntityTypeBuilder<UserServer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new UserServer() { Id = new Guid(""), UserId = new Guid("b995b0f3-f7da-4878-ae1a-d3a667b79906"), ServerId= new Guid("caec2ebb-a150-45f1-996f-7e89ec5f4028") });
            builder.HasData(new UserServer() { Id = new Guid(""), UserId = new Guid("b995b0f3-f7da-4878-ae1a-d3a667b79906"), ServerId= new Guid("f06ab99c-ab02-4c93-b788-4d5a7211b694") });
        }

    }
}
