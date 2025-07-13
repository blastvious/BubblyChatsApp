
using BubllyChatServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SignalR service
builder.Services.AddSignalR();

// Add CORS - QUAN TRỌNG cho client kết nối được
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Sử dụng CORS - PHẢI ĐẶT TRƯỚC UseAuthorization
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Map SignalR hub - Đường dẫn nên nhất quán (chữ thường)
app.MapHub<ChatHub>("/chathub");
app.MapHub<CallHub>("/callhub");

//app.MapHub<CallHub>("/callhub");

app.Run();