using DevExpress.Xpo.DB;
using DxGrid.BindToDataUsingXPO.WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddXpoDefaultDataLayer(ServiceLifetime.Singleton, dl => dl
    .UseConnectionString(builder.Configuration.GetConnectionString("WideWorldImportersExample"))
    .UseThreadSafeDataLayer(true)
    .UseConnectionPool(false) // Remove this line if you use a database server like SQL Server, Oracle, PostgreSql, etc.
    .UseAutoCreationOption(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema) // Remove this line if the database already exists
    .UseEntityTypes(typeof(People), typeof(Orders), typeof(Customers)) // Pass all of your persistent object types to this method.
);
builder.Services.AddXpoDefaultUnitOfWork();

builder.Services.AddCors(options => {
    options.AddPolicy("BlazorPolicy", policy => {
        policy.WithOrigins("https://localhost:7226")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
