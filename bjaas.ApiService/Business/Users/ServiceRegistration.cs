namespace bjaas.ApiService.Business.Users
{
    using bjaas.ApiService.Business.Users.Services;
    using bjaas.ApiService.Business.Users.Services.Contracts;

    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<JwtService>();
            services.AddScoped<LocalServerSideAuthenticationService>();
            services.AddScoped<IUsersService, UsersService>();
        }
    }
}
