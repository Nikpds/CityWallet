﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
