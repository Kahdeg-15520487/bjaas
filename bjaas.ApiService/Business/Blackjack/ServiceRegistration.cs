namespace bjaas.ApiService.Business.Blackjack
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBlackjackService, BlackjackService>();
        }
    }
}
