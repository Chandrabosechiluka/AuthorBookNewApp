using AuthorBookNewApp.Data;
using AuthorBookNewApp.Exceptions;
using AuthorBookNewApp.Repositories;
using AuthorBookNewApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyAppContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));



builder.Services.AddControllers();
builder.Services.AddExceptionHandler<AppExceptionHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
                        options.
                        AddPolicy("FrontendConnection", policy => policy.WithOrigins("http://localhost:5173").
                        AllowAnyMethod().
                        AllowAnyHeader().
                        AllowCredentials())
                        );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("FrontendConnection");
app.UseExceptionHandler(_ => { });
app.UseAuthorization();

app.MapControllers();

app.Run();
