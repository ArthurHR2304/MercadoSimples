using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MercadoSimples.Data;
using MercadoSimples.Models;

namespace MercadoSimples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        
        }


/*==========================================================================================================================*/

        // GET: api/Produtos (Listar todos)
        [HttpGet("Listas Produto")]
        public async Task<ActionResult> GetProdutos(int pagina = 1, int tamanhoPagina = 10, double? precoMinimo = null)
        {

            // Garante que a página não seja zero ou negativa
            if (pagina <= 0) pagina = 1;

            // AQUI ESTÁ O SEGREDO: 
            // Você define a query já avisando que quer incluir o estoque!
            var query = _context.Produtos
                .Include(p => p.Estoque) // <-- Adicione isso aqui
                .AsQueryable();

            // 1. Aplicamos o filtro se o usuário enviou um preço 💰
            // Nota: Se você mudou a Model para 'decimal' (sem o ?), remova o '.HasValue' de p.Preco
            if (precoMinimo.HasValue)
            {
                query = query.Where(p => p.Preco >= precoMinimo.Value);
                query = query.Where(p => p.Ativo == true);
            }

            // 2. Contamos o total baseado na query JÁ FILTRADA
            var totalProdutos = await query.CountAsync();

            // 3. Buscamos os produtos usando a MESMA query do filtro
            var produtos = await query
                .Include(p => p.Estoque) // <-- E aqui também, para garantir que o estoque seja carregado
                .Where(p => p.Ativo == true) // Filtra apenas os produtos ativos
                .Skip((pagina - 1) * tamanhoPagina) // Pula os itens das páginas anteriores
                .Take(tamanhoPagina)               // Pega apenas a quantidade da página
                .ToListAsync();



            // 4. Retorno formatado para o seu Front-end/Swagger
            return Ok(new
            {
                TotalItens = totalProdutos,
                PaginaAtual = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalPaginas = (int)Math.Ceiling((double)totalProdutos / tamanhoPagina),
                Dados = produtos
            });
        }

        // GET: api/Produtos/buscar?nome=arroz (Busca por nome)
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosPorNome(string nome)
        {

            // 1. Se o nome estiver vazio, retorna todos ou uma lista vazia (decisão de design)
            if (string.IsNullOrEmpty(nome))
            { 
                return await _context.Produtos.ToListAsync();
            }

            // 2. A mágica do LINQ: Traduz para "SELECT * FROM Produtos WHERE Nome LIKE '%nome%'"
            var resultado = await _context.Produtos
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
                .ToListAsync();

            return Ok(resultado);

        }

        



/*==========================================================================================================================*/

        // POST: api/Produtos (Cadastrar um novo)
        [HttpPost("Cadastrar Produto")]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, produto);
        }

/*==========================================================================================================================*/

        // DELETE: api/Produtos/{id} (Remover um produto)
        [HttpDelete("{id}/Apagar")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produtoExistente = await _context.Produtos.FindAsync(id);

            if (produtoExistente == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produtoExistente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete : api/Produtos/{id}/Desativar Produto "suave": Apenas marca o produto como inativo, sem deletar do banco
        [HttpDelete("{id}/Desativar")]
        public async Task<IActionResult> DesativarProduto(int id)
        {
            var produtoExistente = await _context.Produtos.FindAsync(id);
            if (produtoExistente == null)
            {
                return NotFound();
            }
            produtoExistente.Ativo = false; // Marca como inativo
            await _context.SaveChangesAsync(); // Salva as mudanças
            return NoContent(); // Retorna 204 (Sucesso, mas sem conteúdo no corpo)
        }



        /*==========================================================================================================================*/

        // PUT: api/Produtos/{id} (Atualiza produtos)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            // 1. Verifica se o ID da URL é o mesmo do corpo da requisição
            if (id != produto.Id)
            {
                return BadRequest("O ID do produto não corresponde ao ID enviado na URL.");
            }

            // 2. Avisa o Entity Framework que esse objeto foi modificado
            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                // 3. Tenta salvar as mudanças no MySQL
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // 4. Caso o produto tenha sido deletado por outra pessoa enquanto você editava
                if (!ProdutoExists(id))
                {
                    return NotFound("Produto não encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Retorna 204 (Sucesso, mas sem conteúdo no corpo)
        }

        // Método auxiliar para checar se o produto existe
        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}

