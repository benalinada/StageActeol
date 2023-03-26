using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistance.Configurations
{
    public  class ServerConfiguration : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new Server() { Id = new Guid("caec2ebb-a150-45f1-996f-7e89ec5f4028"),Name = "DatabaseTest1", ConnexionString= "DESKTOP-0159C82\\VE_SERVER" });
            builder.HasData(new Server() { Id = new Guid("f06ab99c-ab02-4c93-b788-4d5a7211b694"),Name = "DatabaseTest2", ConnexionString= "DESKTOP-0159C82\\VE_SERVER" });
        }
            
    }
}
