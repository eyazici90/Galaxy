using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Bootstrapping
{
    public interface IBootstrapContainer<TContainer> where TContainer : class
    {
        TContainer RegisterGalaxyContainer();
    }
}
