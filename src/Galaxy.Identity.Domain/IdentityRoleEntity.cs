using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Identity
{
    public abstract class IdentityRoleEntity : IdentityRoleEntity<int>, IAggregateRoot, IEntity
    {
    }
}
