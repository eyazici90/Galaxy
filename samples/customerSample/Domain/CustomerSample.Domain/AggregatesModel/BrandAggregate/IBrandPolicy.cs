using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate
{
   public interface IBrandPolicy : IPolicy // Conceptual rules known as Strategy design also 
    {
        Task<bool> CheckAssignment(int brandId);
        int MaxAmountofMarchantperBrand { get; }
    }
}
