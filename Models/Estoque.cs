using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MercadoSimples.Models
{
    public class Estoque
    {
        public int Id { get; set; }

        // O segredo da Foreign Key:
        public int ProdutoId { get; set; }
        [JsonIgnore]
        public Produto? Produto { get; set; } = null!; // Propriedade de navegação

        public string NomeEstoque { get; set; } = string.Empty;
        public string Fornecedor { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public double Peso { get; set; } // Dica: use decimal para dinheiro/peso
        public double Custo { get; set; }

    }
}
