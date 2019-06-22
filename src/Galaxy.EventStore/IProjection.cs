using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public interface IProjection<TNotification> : INotificationHandler<TNotification> where TNotification : INotification
    {

    }
}
