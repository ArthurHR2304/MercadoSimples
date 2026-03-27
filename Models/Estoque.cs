using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MercadoSimples.Models
{
    public class Estoque
    {
        public int Id { get; set; }

        // O segredo da Foreign Key:
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!; // Propriedade de navegação

        public string NomeEstoque { get; set; } = string.Empty;
        public string Fornecedor { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal? Peso { get; set; } // Dica: use decimal para dinheiro/peso
        public decimal? Custo { get; set; }

    }
}
