using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MercadoSimples.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; } /*Este é o número (ID) do produto. O Entity Framework entende que isso é uma "Chave Estrangeira" (Foreign Key) automaticamente.*/

        // O required força você a preencher com algo 
        public required Produto Produto { get; set; } = new Produto(); /*Esta é uma "propriedade de navegação". Ela permite que, no código, você faça algo como estoque.Produto.Nome para acessar o nome do produto diretamente pelo objeto de estoque.*/
        public string NomeEstoque { get; set; }
        public string Fornecedor { get; set; }
        public string Categoria { get; set; }
        public int Quantidade { get; set; }
        public double? Peso { get; set; } = null; 
        public double? Custo { get; set; } = null;
        
    }
}
