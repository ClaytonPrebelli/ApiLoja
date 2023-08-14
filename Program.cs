using ApiLoja.Data;
using ApiLoja.Repositories;
using ApiLoja.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("Politica", builder =>
{
    builder.AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader()
   .AllowAnyOrigin()
   .DisallowCredentials();
}));
string stringConexao = "Server=mysql.prebellisolucoes.com;Database=prebellisoluco08;Uid=prebellisoluco08;Pwd=Gadu1708;charset=utf8";
builder.Services.AddDbContext<DataContext>(opt => opt.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao)));
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IFotosRepository, FotosRepository>();
//repository
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("Politica");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseAuthorization();
app.Run();
