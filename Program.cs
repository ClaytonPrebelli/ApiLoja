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
    builder
    .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader()
   .DisallowCredentials();
}));
string stringConexao = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(opt => opt.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao)));
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IFotosRepository, FotosRepository>();
builder.Services.AddScoped<INoticiasRepository, NoticiasRepositoy>();
builder.Services.AddScoped<ICandidatosRepository, CandidatosRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IFamiliaresRepository, FamiliaresRepository>();
builder.Services.AddScoped<ILivrosRepository, LivrosRepository>();
builder.Services.AddScoped<IFrequenciaRepository, FrequenciaRepository>();
builder.Services.AddScoped<IFinanceiroRepository, FinanceiroRepository>();
builder.Services.AddScoped<ICobrancasRepository, CobrancasRepository>();
builder.Services.AddScoped<IComunicadosRepository, ComunicadosRepository>();
//repository
var app = builder.Build();

// Configure the HTTP request pipeline.
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", context.Request.Headers["Origin"]);
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization, Accept");
        context.Response.StatusCode = 200;
        return;
    }
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.Headers.Add("Access-Control-Allow-Origin", context.Request.Headers["Origin"]);
        await context.Response.WriteAsync($"Erro: {ex.GetType().Name} - {ex.Message}\n{ex.StackTrace}");
    }
});
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("Politica");
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
