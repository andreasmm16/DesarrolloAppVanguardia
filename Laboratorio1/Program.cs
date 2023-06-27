using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;
using VideoGamesShop.Api;
using VideoGamesShop.Core.Interfaces;
using VideoGamesShop.Infrastructure;
using VideoGamesShop.Infrastructure.EntityFramework;
using VideoGamesShop.Infrastructure.EntityFramework.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VideoGamesShopContext>(options =>
  options.UseSqlite("DataSource=videogamesshop.db")
               .EnableSensitiveDataLogging()
               .LogTo(message => Debug.Write(message))
);
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IVideoGameService, VideoGameService>();
builder.Services.AddScoped<IShopRecordService, ShopRecordService>();
builder.Services.AddScoped<ErrorResult>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
