using Infra;
using Core;

var builder = WebApplication.CreateBuilder(args);

// 
builder.Services.AddControllers();

// 
builder.Services.AddCoreServices();

// เรียกใช้ AddInfraService เพื่อเพิ่มบริการโครงสร้างพื้นฐานทั้งหมด
builder.Services.AddInfraService(builder.Configuration);
// dotnet tool install --global dotnet-ef
// dotnet ef migrations add "Initial" --project ./Infra --startup-project ./API --output-dir ./Database/Migrations
// dotnet ef database update --project ./Infra --startup-project ./API

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();