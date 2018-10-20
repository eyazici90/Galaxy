using CustomerSample.Common.Dtos;
using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using Galaxy.Application;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSample.Application
{
    public class BrandAppService : QueryAppServiceAsync<BrandDto, int, Brand>
    {
        public BrandAppService(IRepositoryAsync<Brand, int> repositoryAsync, IObjectMapper objectMapper) : base(repositoryAsync, objectMapper)
        {
        }

        public override Task<BrandDto> FindAsync(int id)
        {
            return base.FindAsync(id);
        }

        public override IList<BrandDto> GetAll()
        {
            return base.GetAll();
        }

        public override Task<BrandDto> GetAsync(int id)
        {
            return base.GetAsync(id);
        }

        public override IQueryable<BrandDto> Queryable()
        {
            return base.Queryable();
        }

        public override IQueryable<BrandDto> QueryableNoTrack()
        {
            return base.QueryableNoTrack();
        }

        public override IQueryable<BrandDto> QueryableWithNoFilter()
        {
            return base.QueryableWithNoFilter();
        }
    }
}
