using DiplomDb.API;
using DiplomDB.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>()); // Маппинг
builder.Services.MakeBeautifulValidation(); // Валидация DTO
builder.Services.MakeBeautifulCors(); // Подключаем CORS из DI
builder.Services.MakeDbContextInMemory(); // БД в памяти
builder.Services.MakeBeautifulSwagger(); // Swagger
builder.Services.MakeBeautifulServices(); // Сервисный слой
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Diplom DB v1");
    });
    app.UseHsts();
}

app.UseRouting();
app.UseCors("Policy");
app.UseCors("AllowMobileApp");
app.UseAuthorization();
app.MapControllers();
app.Run();
