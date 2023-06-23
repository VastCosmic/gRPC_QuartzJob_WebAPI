var builder = WebApplication.CreateBuilder(args);

// Add services to the container.Z
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ע��gRPC����˼�����ַ
builder.Services.AddSingleton<string>("https://localhost:7259");

// ע����־����
builder.Services.AddLogging(builder =>
{
    builder.AddLog4Net("log4net.config");
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
