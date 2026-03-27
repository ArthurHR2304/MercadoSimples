using System;
using Microsoft.EntityFrameworkCore;
using MercadoSimples.Models; // Para ele reconhecer 'Produto' e 'Estoque'

namespace MercadoSimples.Data
{
    public class AppDbContext : DbContext // Note o 't' no final de DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Suas tabelas do banco de dados
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
    }
}
