﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public interface IStreamNameResolver
    {
        string Resolve(string identifier);
    }
}
