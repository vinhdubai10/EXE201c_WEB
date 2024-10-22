using Microsoft.EntityFrameworkCore;
using WEB_EXE201.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Chuỗi kết nối
var connectionString = builder.Configuration.GetConnectionString("DBcontext");

// Đăng ký ApplicationDbContext
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(connectionString));

// Thêm dòng này
builder.Services.AddHttpClient(); // Đăng ký HttpClient
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapGet("/", async context =>
{
    context.Response.Redirect("/Home/Index");
    await Task.CompletedTask;
});
app.MapRazorPages();

app.Run();
