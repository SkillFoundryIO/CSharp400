using LibraryManagement.API.Interfaces;
using LibraryManagement.API.Repositories;

namespace LibraryManagement.API.Extensions
{
    public static class DIRegistrationExtensions
    {
        public static WebApplicationBuilder AddBorrowerRepositories(
            this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IBorrowerRepository, BorrowerRepository>();
            // add other repositories here
            return builder;
        }
    }
}
