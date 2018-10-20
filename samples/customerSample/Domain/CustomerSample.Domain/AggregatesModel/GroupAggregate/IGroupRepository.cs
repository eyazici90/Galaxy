
using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSample.Customer.Domain.AggregatesModel.GroupAggregate
{
    public interface IGroupRepository : ICustomRepository
    {
        void Add(Group group);

        void Update(Group group);

        Task<Group> GetAsync(int groupId);
    }

}
