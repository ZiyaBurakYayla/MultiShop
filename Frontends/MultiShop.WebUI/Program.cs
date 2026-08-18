using Microsoft.AspNetCore.Mvc.Razor;
using MultiShop.WebUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebUIServices(builder.Configuration);

builder.Services.AddControllersWithViews(opt =>
{
    opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});

builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/500");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

var supportedCulture = new[] { "en", "it", "fr", "de", "ru", "tr" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCulture[5]).AddSupportedCultures
    (supportedCulture).AddSupportedUICultures(supportedCulture);
app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
