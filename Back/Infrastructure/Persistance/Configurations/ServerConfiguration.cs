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
            builder.HasData(new Server() { Id = new Guid("caec2ebb-a150-45f1-996f-7e89ec5f4028"),Name = "Server1", ConnexionString= "DESKTOP-0159C82\\VE_SERVER" ,type= "Engine" });
            builder.HasData(new Server() { Id = new Guid("f06ab99c-ab02-4c93-b788-4d5a7211b694"),Name = "Server2", ConnexionString= "DESKTOP-E4EOVQQ\\VE_SERVER", type = "Engine" });
            builder.HasData(new Server() { Id = new Guid("aca3c183-8428-484c-945a-aa46883bf669"), Name = "Server_Analyse1", ConnexionString = "localhost", type = "Analyse" });
           // builder.HasData(new Server() { Id = new Guid("112743c8-2dca-41f5-8a40-9ace55516c47"), Name = "Server_Analyse2", ConnexionString = "DESKTOP-E4EOVQQ\\VE_SERVER", type = "Analyse" });


        }
            
    }
}
