using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore.Bootstrapper.AutoFacModules
{ 
    public class SnapshotReaderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SnapshotReaderAsync>()
                        .As<IAsyncSnapshotReader>()
                        .InstancePerDependency(); 
        }
    }
}
