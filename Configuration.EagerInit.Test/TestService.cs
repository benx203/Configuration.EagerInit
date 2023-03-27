using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.EagerInit.Tests
{
    [EagerInit]
    internal class TestService
    {
        private static bool _inited = false;
        public TestService() { 
            _inited = true;
        }

        public static bool IsInit()
        {
            return _inited;
        }
    }
}
