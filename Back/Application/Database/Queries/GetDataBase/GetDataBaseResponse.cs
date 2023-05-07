using Domain.Common.Models;

namespace Application.Database.Queries.GetDataBase
{
    public class GetDataBaseResponse
    {
        public IEnumerable<DataBase> DataBases { get; set; }
    }
}
