using Domain.Common.Models;

namespace Application.Database.Queries.GetDataBaseSql
{
    public class GetDataBaseResponse
    {
        public IEnumerable<DataBase> DataBases { get; set; }
    }
}
