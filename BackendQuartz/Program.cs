using Infrastructure.Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.JodsRegister(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

await app.UseQuartzAsync();

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
