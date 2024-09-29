using Abbas_Behjatnia.CTC.EFCore;
using Abbas_Behjatnia.Shared.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("swagger/v1/swagger.json", "Abbas_Behjatnia.CTC.HttpApi.Host v1");
        c.RoutePrefix = "";
    });
}

app.InitializeApplication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

