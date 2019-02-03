﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EntityFrameworkCore
{
    public static class GalaxyDbConnectionParameters
    {
        public static string DefaultConnection { get; set; }
        public static string CommandTimeout { get; set; }

        public static string Options { get;}
        public static string AppSession { get; }
        
    }
}
