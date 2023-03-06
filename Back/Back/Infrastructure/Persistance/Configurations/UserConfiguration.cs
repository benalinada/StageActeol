using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistance.Configurations
{
    public  class UserConfiguration
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
        }
            
    }
}
