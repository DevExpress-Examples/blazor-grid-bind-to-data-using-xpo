using DxGrid.BindToDataUsingXPO.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDevExpressBlazor(options => {
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});
builder.Services.AddMvc();

builder.Services.AddHttpClient("XpoApi", client => {
    var baseAddress = builder.Configuration["XpoApi:BaseAddress"]
        ?? throw new InvalidOperationException("Configure 'XpoApi:BaseAddress' in appsettings.json to point at the WebAPI project's URL.");
    client.BaseAddress = new Uri(baseAddress);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AllowAnonymous();

app.Run();