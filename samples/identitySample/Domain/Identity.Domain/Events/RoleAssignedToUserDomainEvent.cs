using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.Events
{
    public class RoleAssignedToUserDomainEvent : INotification
    {
        public User User { get; private set; }

        public RoleAssignedToUserDomainEvent(User user)
        {
            this.User = user;
        }
    }
}
