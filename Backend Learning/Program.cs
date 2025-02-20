using Backend_Learning.Data;
using Backend_Learning.Services;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<StudentService>();



builder.Services.AddDbContext<StudentContext>(options => options.UseNpgsql(configuration.GetConnectionString("SupabaseDatabase")));




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



using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<StudentContext>();
    dbContext.Database.Migrate();
}



app.Run();
