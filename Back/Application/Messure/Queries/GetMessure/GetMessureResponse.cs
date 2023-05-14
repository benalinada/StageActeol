
using Domain.Common.Models;
namespace Application.Messure.Queries.GetMessure
{
    public class GetMessureResponse
    {
        public string DbCubeName { get; set; }
        public IEnumerable<MesuureDto> Messure { get; set; }
    }
    public class MesuureDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

