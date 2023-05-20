using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Configuration.EagerInit.Tests
{
    public class EagerInitTest
    {

        [Fact]
        public void TestEagerInit()
        {
            Assert.False(TestService.IsInit());

            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.UseEagerInit();
                })
                .Build();
            host.RunAsync();

            Thread.Sleep(1000);

            Assert.True(TestService.IsInit());
        }
    }
}
