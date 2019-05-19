using EventStore.ClientAPI.SystemData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public interface IStreamUserCredentialsResolver
    {
        UserCredentials Resolve(string identifier);
    }
}
