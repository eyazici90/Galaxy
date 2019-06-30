using Galaxy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{

    public abstract class EntityState<TState, TPrimaryKey> : Entity<TPrimaryKey>, IObjectState, IEntity<TPrimaryKey>
           where TState : class
    {
        public abstract string GetStreamName(string id);
        public TState With(TState state, Action<TState> update)
        {
            update(state);
            return state;
        }
    }
}
