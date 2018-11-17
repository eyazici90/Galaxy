using Galaxy.Domain.Auditing;
using Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.AggregatesModel.PermissionAggregate
{
    public sealed class Permission : FullyAuditAggregateRootEntity
    {
        public int Id { get; private set; }
        public int? ParentId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string EventName { get; private set; }
        public string ControllerName { get; private set; }
        public string ActionName { get; private set; }
        public string AreaName { get; private set; }
        public string Url { get; private set; }
        public bool IsGlobal { get; private set; }
        public bool IsMenu { get; private set; }
        public string Icon { get; private set; }

        private Permission()
        {
        }

        private Permission(string name) : this()
        {
            this.Name = !string.IsNullOrWhiteSpace(name) ? name
                                                         : throw new ArgumentNullException(nameof(name));
        }

        public static Permission Create(string name)
        {
            return new Permission(name);
        }

        public Permission ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new IdentityDomainException($"Invalid Permission Name: {name}");
            this.Name = name;
            return this;
        }

        public Permission ChangeOrSetDesciption(string desc)
        {
            //if (string.IsNullOrWhiteSpace(desc))
            //    throw new IdentityDomainException($"Invalid Permission Desc: {desc}");
            this.Description = desc;
            return this;
        }

        public Permission ChangeOrSetUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new IdentityDomainException($"Invalid Permission Url: {url}");
            this.Url = url;
            return this;
        }
    }
}
