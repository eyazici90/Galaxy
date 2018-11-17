using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.Events
{
    public class PermissionAssignedToUserDomainEvent : INotification
    {
        public User User { get; private set; }


        public PermissionAssignedToUserDomainEvent(User user)
        {
            this.User = user;
        }
    }
}
