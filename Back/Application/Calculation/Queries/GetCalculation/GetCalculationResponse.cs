
using Domain.Common.Models;
namespace Application.Calculation.Queries.GetCalculation
{
    public class GetCalculationResponse
    {
        public string DbCubeName { get; set; }
        public IEnumerable<CalculationeDto> Calculation { get; set; }
    }
    public class CalculationeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

