
using CustomerSample.Customer.Domain.AggregatesModel.GroupAggregate;
using System;
using System.Threading.Tasks;

namespace CustomerSample.Customer.Domain.EFRepositories.GroupAggregate
{
    public class GroupRepository : IGroupRepository
    {
        public void Add(Group group)
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetAsync(int groupId)
        {
            throw new NotImplementedException();
        }

        public void Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
