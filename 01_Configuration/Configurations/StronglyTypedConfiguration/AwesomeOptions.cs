﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StronglyTypedConfiguration
{
    public class AwesomeOptions
    {
        public string Foo { get; set; }
        public string Bar { get; set; }
        public BazOptions Baz { get; set; }
        public class BazOptions
        {
            public string Foo { get; set; }
        }
    }
}
