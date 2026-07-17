


using ApiLoja.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLoja.Data
{
    public class DataContext: DbContext

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CargosModels>().HasData(
                new CargosModels { Id = 1, Nome = "Venerável Mestre" },
                new CargosModels { Id = 2, Nome = "Tesoureiro" },
                new CargosModels { Id = 3, Nome = "Secretário" }
            );

            modelBuilder.Entity<CategoriasCobrancasModels>().HasData(
                new CategoriasCobrancasModels { Id = 1, CategoriaNome = "Mensalidade" },
                new CategoriasCobrancasModels { Id = 2, CategoriaNome = "Mútua" },
                new CategoriasCobrancasModels { Id = 3, CategoriaNome = "Joia" },
                new CategoriasCobrancasModels { Id = 4, CategoriaNome = "Medalha" },
                new CategoriasCobrancasModels { Id = 5, CategoriaNome = "Placets" },
                new CategoriasCobrancasModels { Id = 6, CategoriaNome = "Outros" }
            );

            modelBuilder.Entity<CategoriaFinanceiroModels>().HasData(
                new CategoriaFinanceiroModels { Id = 1, Nome = "Mensalidade", Tipo = "Entrada" },
                new CategoriaFinanceiroModels { Id = 2, Nome = "Mútua", Tipo = "Entrada" },
                new CategoriaFinanceiroModels { Id = 3, Nome = "Joia", Tipo = "Entrada" },
                new CategoriaFinanceiroModels { Id = 4, Nome = "Medalha", Tipo = "Entrada" },
                new CategoriaFinanceiroModels { Id = 5, Nome = "Aluguel", Tipo = "Saida" },
                new CategoriaFinanceiroModels { Id = 6, Nome = "Capitação GOSP", Tipo = "Saida" },
                new CategoriaFinanceiroModels { Id = 7, Nome = "Mútua", Tipo = "Saida" },
                new CategoriaFinanceiroModels { Id = 8, Nome = "Insumos", Tipo = "Saida" },
                new CategoriaFinanceiroModels { Id = 9, Nome = "Ágape", Tipo = "Saida" },
                new CategoriaFinanceiroModels { Id = 10, Nome = "Medalha", Tipo = "Saida" },
                new CategoriaFinanceiroModels { Id = 11, Nome = "Placets", Tipo = "Saida" }
            );
        }

        public DbSet<UsuarioModels> Usuario { get; set; }
        public DbSet<FamiliaresModels> Familiares { get; set;}
        public DbSet<StatusModels> Status { get; set; }
     
        public DbSet<FotosNoticiaModels> FotosNoticias { get; set; }
        public DbSet<DocumentosModels> Documentos { get; set; }
        public DbSet<NoticiasModels> Noticias { get; set; }
        public DbSet<FotosCandidatoModels> FotosCandidato { get; set; }
        public DbSet<CandidatosModels> Candidatos { get; set;}
        public DbSet<TokenModels> Token { get; set; }
        public DbSet<LivrosModels> Livros { get; set; }
        public DbSet<CargosModels> Cargos { get; set; }
        public DbSet<CobrancasModels> Cobrancas { get; set; }
        public DbSet<CategoriasCobrancasModels> CategoriasCobrancas { get; set; }
        public DbSet<FrequenciaModels> Frequencia { get; set; }
        public DbSet<FinanceiroModels> Financeiro { get; set; }
        public DbSet<CategoriaFinanceiroModels> CategoriasFinanceiro { get; set; }
        public DbSet<ComunicadosModels> Comunicados { get; set; }
        public DbSet<ComunicadosFotoModels> ComunicadosFotos { get; set; }

    }
}
