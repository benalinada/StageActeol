using Domain.Common.Models;

namespace Application.Database.Queries
{
    public class GetDataBaseResponse
    {
        public IEnumerable<DataBase> DataBases { get; set; }
    }
}
