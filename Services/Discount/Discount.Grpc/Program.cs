using Discount.Grpc;
using Discount.Grpc.Data;
using Discount.Grpc.Interceptors;
using Discount.Grpc.Services;
using Discount.Grpc.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc((opt) =>
{
    opt.Interceptors.Add<ValidationInterceptor>();
});
builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});
builder.Services.AddSingleton<IValidator<CreateDiscountRequest>, CreateDiscountValidator>();
builder.Services.AddSingleton<IValidator<UpdateDiscountRequest>, UpdateDiscountValidator>();
builder.Services.AddSingleton<IValidator<GetDiscountRequest>, GetDiscountValidator>();
builder.Services.AddSingleton<IValidator<DeleteDiscountRequest>, DeleteDiscountValidator>();


builder.Services.AddSingleton<ValidationInterceptor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
