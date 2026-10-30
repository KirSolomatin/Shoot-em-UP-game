using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public static class TestSetup
    {
        [AssemblyInitialize]
        public static void Init(TestContext _)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
        }
    }
}
