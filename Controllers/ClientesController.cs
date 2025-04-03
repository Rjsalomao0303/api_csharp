using CadastroClientes.Data; // Importa o namespace que contém o AppDbContext para acessar o banco de dados.
using Microsoft.AspNetCore.Mvc; // Importa o namespace para classes relacionadas ao ASP.NET Core MVC.
using Microsoft.EntityFrameworkCore; // Importa o namespace para classes relacionadas ao Entity Framework Core.
using System.Collections.Generic; // Importa o namespace para usar listas genéricas.
using System.Threading.Tasks; // Importa o namespace para usar programação assíncrona.
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace CadastroClientes.Controllers // Define o namespace para o controlador ClienteController.
{
    [Authorize] 
    [ApiController] // Indica que esta classe é um controlador de API.
    [Route("api/clientes")] // Define a rota base para todas as ações neste controlador.
    public class ClienteController : ControllerBase // Declara a classe ClienteController, que herda de ControllerBase.
    {
        private readonly AppDbContext _context; // Declara um campo privado somente leitura para o contexto do banco de dados.

        public ClienteController(AppDbContext context) // Construtor da classe ClienteController, que recebe o contexto do banco de dados por injeção de dependência.
        {
            _context = context; // Atribui o contexto recebido ao campo _context.
        }


        // Criar um novo cliente
        [HttpPost] // Indica que esta ação responde a requisições HTTP POST.
        public async Task<IActionResult> CriarCliente(Cliente cliente) // Declara a ação CriarCliente, que cria um novo cliente.
        {

            if (await _context.Clientes.AnyAsync(c => c.cnpj == cliente.cnpj)) // Verifica se já existe um cliente com o mesmo CNPJ.
                return BadRequest("CNPJ já cadastrado!"); // Retorna um erro BadRequest se o CNPJ já estiver cadastrado.

            _context.Clientes.Add(cliente); // Adiciona o novo cliente ao contexto do banco de dados.
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados de forma assíncrona.
            return CreatedAtAction(nameof(ObterCliente), new { id = cliente.id }, cliente); // Retorna um CreatedAtAction com o cliente criado.
        }

        // Listar todos os clientes
        [HttpGet] // Indica que esta ação responde a requisições HTTP GET.
        public async Task<ActionResult<IEnumerable<Cliente>>> ListarClientes() // Declara a ação ListarClientes, que lista todos os clientes.
        {
            return await _context.Clientes.ToListAsync(); // Retorna uma lista de todos os clientes do banco de dados de forma assíncrona.
        }

        // Buscar cliente por ID
        [HttpGet("{id}")] // Indica que esta ação responde a requisições HTTP GET com um parâmetro ID na rota.
        public async Task<ActionResult<Cliente>> ObterCliente(int id) // Declara a ação ObterCliente, que busca um cliente por ID.
        {
            var cliente = await _context.Clientes.FindAsync(id); // Busca o cliente com o ID especificado no banco de dados de forma assíncrona.
            if (cliente == null) return NotFound("Cliente não encontrado"); // Retorna um erro NotFound se o cliente não for encontrado.
            return cliente; // Retorna o cliente encontrado.
        }

        // Editar cliente
        [HttpPut("{id}")] // Indica que esta ação responde a requisições HTTP PUT com um parâmetro ID na rota.
        public async Task<IActionResult> EditarCliente(int id, Cliente clienteAtualizado) // Declara a ação EditarCliente, que edita um cliente existente.
        {
            var cliente = await _context.Clientes.FindAsync(id); // Busca o cliente com o ID especificado no banco de dados de forma assíncrona.
            if (cliente == null) return NotFound("Cliente não encontrado"); // Retorna um erro NotFound se o cliente não for encontrado.

            if (await _context.Clientes.AnyAsync(c => c.cnpj == clienteAtualizado.cnpj && c.id != id)) // Verifica se já existe outro cliente com o mesmo CNPJ (exceto o cliente que está sendo editado).
                return BadRequest("CNPJ já cadastrado!"); // Retorna um erro BadRequest se o CNPJ já estiver cadastrado.

            cliente.nome = clienteAtualizado.nome; // Atualiza o nome do cliente.
            cliente.cnpj = clienteAtualizado.cnpj; // Atualiza o CNPJ do cliente.
            cliente.endereco = clienteAtualizado.endereco; // Atualiza o endereço do cliente.
            cliente.telefone = clienteAtualizado.telefone; // Atualiza o telefone do cliente.

            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados de forma assíncrona.
            return NoContent(); // Retorna um NoContent indicando que a edição foi bem-sucedida.
        }

        // Remover cliente
        [HttpDelete("{id}")] // Indica que esta ação responde a requisições HTTP DELETE com um parâmetro ID na rota.
        public async Task<IActionResult> RemoverCliente(int id) // Declara a ação RemoverCliente, que remove um cliente.
        {
            var cliente = await _context.Clientes.FindAsync(id); // Busca o cliente com o ID especificado no banco de dados de forma assíncrona.
            if (cliente == null) return NotFound("Cliente não encontrado"); // Retorna um erro NotFound se o cliente não for encontrado.

            _context.Clientes.Remove(cliente); // Remove o cliente do contexto do banco de dados.
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados de forma assíncrona.
            return NoContent(); // Retorna um NoContent indicando que a remoção foi bem-sucedida.
        }
    }
}