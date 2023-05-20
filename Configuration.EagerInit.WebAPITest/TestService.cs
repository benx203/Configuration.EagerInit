namespace Configuration.EagerInit.WebAPITest
{
    [EagerInit(ServiceLifetime.Singleton)]
    public class TestService
    {

        public TestService() {
            Console.WriteLine("init...");
        }
    }
}
