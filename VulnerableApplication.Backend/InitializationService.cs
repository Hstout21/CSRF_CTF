using Microsoft.Extensions.DependencyInjection;

namespace VulnerableApplication.Backend
{
    public static class InitializationService
    {
        public static void InitializeServices(IServiceCollection service)
        {
            service.AddTransient<IBackend, Backend>();
        }
    }
}
