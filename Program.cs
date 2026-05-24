using Scalar.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlpath);
}    
);


var MyAllowOrigins = "MyAllowOrigins";
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowOrigins, p =>
    {
        p.AllowAnyHeader();
        //p.AllowAnyOrigin();
        p.WithOrigins();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Middlewares para desenvolvimento
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
}
//should has this order

app.UseCors(MyAllowOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();
//add custom middlewares 

app.Use(async (context,next)=>
{
    var path = context.Request.Path;
    Console.WriteLine($"Request:{ path}");
    await next.Invoke();//indica salto a siguiente middleware
    Console.WriteLine($"Response:{context.Response.StatusCode}");
});
app.MapControllers();

app.Run();
