using ResumeBaseDAL;
using ResumeBaseBLL;
using ResumeBaseBLL.Models;

var builder = WebApplication.CreateBuilder(args);

// ������ ����������
builder.Services.AddControllers();

// ������ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// �������� ��������� �� ��������� (����� ������ �� Scoped/Transient �� ��������)
builder.Services.AddSingleton<IRepository<Resume>, ResumeRepository>();
builder.Services.AddSingleton<IRepository<Vacancy>, VacancyRepository>();
builder.Services.AddSingleton<IRepository<Application>, ApplicationRepository>();

// �������� ������
builder.Services.AddScoped<ResumeService>();
builder.Services.AddScoped<VacancyService>();
builder.Services.AddScoped<ApplicationService>();

var app = builder.Build();

// ��������� Swagger � ��� ����������� (����� �������� ���� ��� dev)
app.UseSwagger();
app.UseSwaggerUI();

// HTTPS �������� (�������)
app.UseHttpsRedirection();

// ����������� (�� ����'������ ��� �������� API, ��� ��� ����)
app.UseAuthorization();

// ������������� �� ����������
app.MapControllers();

app.Run();
