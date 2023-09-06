

using ApiLoja.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLoja.Data
{
    public class DataContext: DbContext

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UsuarioModels> Usuario { get; set; }
        public DbSet<FamiliaresModels> Familiares { get; set;}
        public DbSet<StatusModels> Status { get; set; }
        public DbSet<FotosModels> Fotos { get; set; }
        public DbSet<FotosLojasModels> FotosLojas { get; set; }

        public DbSet<FotosNoticiaModels> FotosNoticias { get; set; }
        public DbSet<DocumentosModels> Documentos { get; set; }
        public DbSet<LojaModels> Lojas { get; set; }
        public DbSet<NoticiasModels> Noticias { get; set; }
    }
}
