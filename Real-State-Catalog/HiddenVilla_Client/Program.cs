using Blazored.LocalStorage;
using Real_State_Catalog_Client.Service;
using Real_State_Catalog_Client.Service.IService;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using HiddenVilla_Client;
using System.Threading.Tasks;
using Real_State_Catalog_Client;


namespace Real_State_Catalog_Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress =
                        new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl"))});
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
            builder.Services.AddScoped<IHotelAmenityService, HotelAmenityService>();
            builder.Services.AddScoped<IRoomOrderDetailsService, RoomOrderDetailsService>();
            builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();
            await builder.Build().RunAsync();
        }
    }
}
