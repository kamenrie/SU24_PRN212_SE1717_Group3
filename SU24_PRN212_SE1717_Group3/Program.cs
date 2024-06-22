using DataAccessLayer.DAO;
using DataAccessLayer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddSession();
builder.Services.AddScoped<AuthDao>();
builder.Services.AddScoped<OrderDAO>();
builder.Services.AddScoped<AccountDAO>();
builder.Services.AddScoped<ProductDAO>();
builder.Services.AddScoped<FeedbackDAO>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddRequestDecompression();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseRequestDecompression();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
