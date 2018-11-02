using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.ConsoleApp.DomainModel
{
    public sealed class UserCreatedDomainEvent : INotification
    {
        public User User { get; private set; }
        public UserCreatedDomainEvent(User user)
        {
            this.User = user;   
        }
    }
}
