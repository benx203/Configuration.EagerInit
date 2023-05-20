# Configuration.EagerInit

#### 介绍
实现.net下依赖注入对象的饥饿初始化

#### 安装教程

dotnet add package Configuration.EagerInit --version 1.0.1

#### 使用说明

TestService
```
    [EagerInit]
    internal class TestService
    {
		
		public TestService()
		{
			Console.WriteLine("Init...");
		}
	
	}
```
Main

```
using IHost host = Host.CreateDefaultBuilder()
        .ConfigureServices(services =>
        {
            services.UseEagerInit();
        })
        .Build();
        await host.RunAsync();
```

