using Microsoft.EntityFrameworkCore;
using N5.Api.Business;
using N5.Api.DataAccess.IBusiness;
using N5.Api.IRepository;
using N5.Api.Context;
using N5.Api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<N5Context>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("N5Database")));

builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();

builder.Services.AddScoped<IPermissionBusinessLogic, PermissionBusinessLogic>();
builder.Services.AddScoped<IPermissionTypeBusinessLogic, PermissionTypeBusinessLogic>();

var app = builder.Build();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "N5 Challenge v1");
    c.RoutePrefix = string.Empty;
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger(x => x.SerializeAsV2 = true);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
