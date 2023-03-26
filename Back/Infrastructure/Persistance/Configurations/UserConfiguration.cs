using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistance.Configurations
{
    public  class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email);
            builder.HasData(new User() { Id = new Guid("b995b0f3-f7da-4878-ae1a-d3a667b79906"),Email = "nada@Email.com", FirstName = "Nada", LastName = "BENALI" });


        }
            
    }
}
