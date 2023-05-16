using Application.Calculation.Commandes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calculation.Commandes
{
    public class CreateCalculationResponse
    {
        public List<CalculationLookUpDto> Data { get; set; }
    }
}
