using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Real_State_Catalog_WCF.Data;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AddContextDB");
builder.Services.AddDbContext<AppContextDb>(options => options.UseSqlServer(connectionString));


builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(3600);
    option.Cookie.IsEssential = true;
});



builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Recipes App Project API",
        Description = "Laboratory work 2022, Ostroh Academy",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "viktoriia.cherevko@oa.edu.ua",
            Name = "Viktoriia Cherevko",
        }
    });
});

builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<InfoDishRepository>();
//builder.Services.AddScoped<CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();