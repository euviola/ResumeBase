using ResumeBaseDAL;
using ResumeBaseBLL;
using ResumeBaseBLL.Models;

var builder = WebApplication.CreateBuilder(args);

// Додаємо контролери
builder.Services.AddControllers();

// Додаємо Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Реєструємо репозиторії як синглтони (можна змінити на Scoped/Transient за потребою)
builder.Services.AddSingleton<IRepository<Resume>, ResumeRepository>();
builder.Services.AddSingleton<IRepository<Vacancy>, VacancyRepository>();
builder.Services.AddSingleton<IRepository<Application>, ApplicationRepository>();

// Реєструємо сервіси
builder.Services.AddScoped<ResumeService>();
builder.Services.AddScoped<VacancyService>();
builder.Services.AddScoped<ApplicationService>();

var app = builder.Build();

// Увімкнення Swagger у всіх середовищах (можна залишити лише для dev)
app.UseSwagger();
app.UseSwaggerUI();

// HTTPS редирект (опційно)
app.UseHttpsRedirection();

// Авторизація (не обов'язково для простого API, але хай буде)
app.UseAuthorization();

// Маршрутизація до контролерів
app.MapControllers();

app.Run();
