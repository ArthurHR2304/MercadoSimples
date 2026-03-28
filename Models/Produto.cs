using System;
using System.ComponentModel.DataAnnotations;

namespace MercadoSimples.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public Estoque? Estoque { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }
        public string Descricao { get; set; }

        [Range(0.01, 999999, ErrorMessage = "O preço deve ser maior que zero.")]
        
        public double Preco { get; set; }
        public double ValorEstoque => Preco * (Estoque?.Quantidade ?? 0); /*Essa propriedade calcula o valor total do estoque multiplicando o preço do produto pela quantidade disponível no estoque. Se o estoque for nulo, ele assume que a quantidade é zero, resultando em um valor de estoque de zero.*/
        public DateTime Validade { get; set; } = DateTime.Now; /*Ela captura o instante exato (data e hora) do relógio do sistema no momento em que a linha de código é executada.*/

        public bool Ativo { get; set; } = true;
    }
}
