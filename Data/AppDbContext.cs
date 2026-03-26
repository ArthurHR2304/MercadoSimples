using System;
using Microsoft.EntityFrameworkCore;
using MercadoSimples.Models;

namespace MercadoSimples.Data
{
    public class AppDbContext: DbContext
    {
        // O DbContext é o coração do Entity Framework
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        
        }
        // Esta linha diz: "Eu quero uma tabela chamada Produtos no banco de dados"
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
    }
}
