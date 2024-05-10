using LegacyApp.Business.Abstract;
using LegacyApp.Business.Concrete;
using LegacyApp.Core.Repositories;
using LegacyApp.Core.Repositories.Abstracts;
using LegacyApp.Core.Utilities.Configs;
using LegacyApp.DataAccess.Abstract;
using LegacyApp.DataAccess.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LegacyApp.Consumer;

internal class Program
{
    static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();

        host.Start();

        // Uygulamanın ana işlevselliğini çalıştır
        var userService = host.Services.GetRequiredService<IUserService>();
        var addResult = userService.AddUser("John", "Doe", "John.doe@gmail.com", new DateTime(1993, 1, 1), 4);
        Console.WriteLine("Adding John Doe was " + (addResult ? "successful" : "unsuccessful"));

        host.Run();
    }

    //public static void AddUser(string[] args)
    //{
    //    // DO NOT CHANGE THIS FILE AT ALL

    //    var userService = new UserService();
    //    var addResult = userService.AddUser("John", "Doe", "John.doe@gmail.com", new DateTime(1993, 1, 1), 4);
    //    Console.WriteLine("Adding John Doe was " + (addResult ? "successful" : "unsuccessful"));
    //}

    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.Configure<WCFSettings>(hostContext.Configuration.GetSection("WCFSettings"));
            services.AddTransient<IUserDal, UserDataAccess>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserCreditService, UserCreditServiceClient>();
            services.AddTransient<IClientRepository, ClientRepository>();
        });
}