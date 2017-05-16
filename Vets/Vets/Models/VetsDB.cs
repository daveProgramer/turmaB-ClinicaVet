using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ClinicaVeterinaria.Models
{
    public class VetsDB : DbContext
    {

        //***********************************************
        //  informar a EF que as classes abeixo descritas
        //  devem ser associadas a uma Base de Dados
        //***********************************************
        public virtual DbSet<Donos> Donos { get; set; }
        public virtual DbSet<Animais> Animais { get; set; }
        public virtual DbSet<Veterinarios> Veterinarios { get; set; }
        public virtual DbSet<Consultas> Consultas { get; set; }


        //***********************************************
        //  onde criar e de que tipo será a Base de Dados
        //***********************************************
        public VetsDB() : base("localizacaoDaBD") { }

        // formatar a configuração das FKs
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // não podemos usar a chave seguinte, nesta geração de tabelas
            // por causa das tabelas do Identity (gestão de utilizadores)
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }
}