using System;
using System.ComponentModel.DataAnnotations;

namespace MercadoSimples.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public decimal Preco { get; set; }
        public DateTime Validade { get; set; } = DateTime.Now; /*Ela captura o instante exato (data e hora) do relógio do sistema no momento em que a linha de código é executada.*/

    }
}
