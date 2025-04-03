using System; // Importa o namespace System, que fornece classes e estruturas fundamentais, como DateTime.
using Microsoft.EntityFrameworkCore; // Importa o namespace EntityFrameworkCore, que fornece funcionalidades para trabalhar com bancos de dados.

namespace CadastroClientes.Data // Define o namespace para as classes de dados, organizando o código.
{
    public class AppDbContext : DbContext // Declara a classe AppDbContext, que herda de DbContext, representando o contexto do banco de dados.
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } // Construtor da classe AppDbContext, que recebe as opções de configuração do DbContext.

        public DbSet<Cliente> Clientes { get; set; } // Declara uma propriedade DbSet<Cliente>, que representa a tabela "Clientes" no banco de dados.

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Sobrescreve o método OnModelCreating, que é chamado durante a criação do modelo do banco de dados.
        {
            modelBuilder.Entity<Cliente>(entity => // Configura a entidade Cliente no modelo.
            {
                entity.ToTable("clientes"); // Define o nome da tabela no banco de dados como "clientes".
                entity.Property(e => e.id).HasColumnName("id"); // Define o nome da coluna "id" na tabela como "id".
                entity.Property(e => e.nome).HasColumnName("nome"); // Define o nome da coluna "nome" na tabela como "nome".
                entity.Property(e => e.cnpj).HasColumnName("cnpj"); // Define o nome da coluna "cnpj" na tabela como "cnpj".
                entity.Property(e => e.dataCadastro).HasColumnName("dataCadastro"); // Define o nome da coluna "dataCadastro" na tabela como "dataCadastro".
                entity.Property(e => e.endereco).HasColumnName("endereco"); // Define o nome da coluna "endereco" na tabela como "endereco".
                entity.Property(e => e.telefone).HasColumnName("telefone"); // Define o nome da coluna "telefone" na tabela como "telefone".
            });
        }
    }

    public class Cliente // Declara a classe Cliente, que representa um cliente.
    {
        public int id { get; set; } // Declara uma propriedade pública inteira id, que representa o identificador único do cliente.
        public string nome { get; set; } // Declara uma propriedade pública string nome, que representa o nome do cliente.
        public string cnpj { get; set; } // Declara uma propriedade pública string cnpj, que representa o CNPJ do cliente.
        public DateTime dataCadastro { get; set; } // Declara uma propriedade pública DateTime dataCadastro, que representa a data de cadastro do cliente.
        public string endereco { get; set; } // Declara uma propriedade pública string endereco, que representa o endereço do cliente.
        public string telefone { get; set; } // Declara uma propriedade pública string telefone, que representa o telefone do cliente.
    }
}