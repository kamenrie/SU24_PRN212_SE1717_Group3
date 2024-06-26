using DataAccessLayer.DAO;
using DataAccessLayer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

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
builder.Services.AddScoped<ShopManagementDAO>();
builder.Services.AddScoped<AdminManagementDAO>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services
	.AddAuthentication(options =>
	{
		options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
	})
	.AddCookie()
	.AddGoogle(GoogleDefaults.AuthenticationScheme, googleOptions =>
	{
		googleOptions.ClientId = "91449811345-vk7d4i3o0k6sijv6hpmqh5dk49letmoc.apps.googleusercontent.com";
		googleOptions.ClientSecret = "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";
		googleOptions.SaveTokens = true;
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
