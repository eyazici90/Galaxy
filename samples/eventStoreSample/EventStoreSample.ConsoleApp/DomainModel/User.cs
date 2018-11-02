using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.ConsoleApp.DomainModel
{
    public sealed class User : AggregateRootEntity
    {
        public string Name { get; private set; }

        public string Surname { get; private set; }

        private User(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
            AddDomainEvent(new UserCreatedDomainEvent(this));
        }

        public static User Create(string name, string surname)
        {
            return new User(name, surname);
        }

       
    }
}
