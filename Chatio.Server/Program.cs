using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Chatio.DataAccess.Data;
using Chatio.Hubs;
using Chatio.Models;
using Chatio.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
    options.DisconnectedCircuitMaxRetained = 100;
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(60);
    options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
    options.MaxBufferedUnacknowledgedRenderBatches = 10;
});
builder.Services.AddResponseCompression(opts => {
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddSingleton<IDictionary<string, ChatUser>>(opts => new Dictionary<string, ChatUser>());
builder.Services.AddScoped<IToastService, ToastService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<TimeZoneService>();

builder.Services.AddDbContext<ChatDbContext>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToPage("/_Host");

var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var serviceScope = serviceScopeFactory.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetService<ChatDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
