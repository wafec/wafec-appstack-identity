﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core
{
    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
