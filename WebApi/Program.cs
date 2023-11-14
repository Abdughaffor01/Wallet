using Infrastructure.AutoMapper;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.CardServices;
using Infrastructure.Services.IdentifiedService;
using Infrastructure.Services.Operation;
using Infrastructure.Services.OwnerServices;
using Infrastructure.Services.TransactionServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var con=builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(c=>c.UseNpgsql(con));
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IOwnerService,OwnerService>();
builder.Services.AddScoped<ITransactionService,TransacationService>();
builder.Services.AddScoped<IOperationService,OperationService>();
builder.Services.AddScoped<ICardService,CardService>();
builder.Services.AddScoped<IIdentifiedService,IdentifiedService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
