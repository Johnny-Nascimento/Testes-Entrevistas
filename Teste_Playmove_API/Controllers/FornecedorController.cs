using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Teste_Playmove_API.Models;
using Teste_Playmove_API.Persistence;
using Teste_Playmove_API.Entities;

namespace Teste_Playmove_API.Controllers
{
    [Route("api/Fornecedores")]
    [ApiController]

    public class FornecedorController : ControllerBase
    {
        private readonly FornecedorDbContext _context;
        private readonly IMapper _mapper;

        public FornecedorController(FornecedorDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todos os fornecedores
        /// </summary>
        /// <returns> Lista de fornecedores </returns>
        /// <response code="200"> Sucesso </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var fornecedor = _context.Fornecedores.Where(d => !d.Excluido).ToList();

            var viewModel = _mapper.Map<List<FornecedorViewModel>>(fornecedor);

            return Ok(viewModel);
        }

        /// <summary>
        /// Retorna um fornecedor específico pelo ID
        /// </summary>
        /// <param name="id"> Identificador único do fornecedor </param>
        /// <returns> Dados do fornecedor buscado pelo Identificador único </returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="404"> Não encontrado </response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var fornecedor = _context.Fornecedores.SingleOrDefault(d => !d.Excluido && d.Id == id);

            if (fornecedor == null)
                return NotFound();

            var viewModel = _mapper.Map<FornecedorViewModel>(fornecedor);

            return Ok(viewModel);
        }

        /// <summary>
        /// Adiciona um novo fornecedor.
        /// </summary>
        /// <remarks>
        /// {"id": 0, "nome": "string", "email": "string", "excluido": true}
        /// </remarks>
        /// <param name="inputFornecedor"> Dados do fornecedor </param>
        /// <returns> Objeto recém criado </returns>
        /// <response code="201"> Sucesso </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(FornecedorInputModel inputFornecedor)
        {
            var fornecedor = _mapper.Map<Fornecedor>(inputFornecedor);

            _context.Fornecedores.Add(fornecedor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = fornecedor.Id}, fornecedor);
        }

        /// <summary>
        /// Atualiza um fornecedor existente pelo ID
        /// </summary>
        /// <remarks>
        /// {"id": 0, "nome": "string", "email": "string", "excluido": true}
        /// </remarks>
        /// <param name="id"> Identificador único do fornecedor </param>
        /// <param name="inputFornecedor"> Dados do fornecedor </param>
        /// <returns> Nada </returns>
        /// <response code="204"> Sucesso </response>
        /// <response code="404"> Não encontrado </response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, FornecedorInputModel inputFornecedor)
        {
            var fornecedorLido = _context.Fornecedores.SingleOrDefault(d => !d.Excluido && d.Id == id);

            if (fornecedorLido == null)
                return NotFound();

            fornecedorLido.Update(inputFornecedor.Nome, inputFornecedor.Email);

            _context.Update(fornecedorLido);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Remove um fornecedor pelo ID.
        /// </summary>
        /// <param name="id"> Identificador único do fornecedor </param>
        /// <returns> Nada </returns>
        /// <response code="204"> Sucesso </response>
        /// <response code="404"> Não encontrado </response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var fornecedorLido = _context.Fornecedores.SingleOrDefault(d => !d.Excluido && d.Id == id);

            if (fornecedorLido == null)
                return NotFound();

            fornecedorLido.Delete();
            _context.SaveChanges();

            return NoContent();
        }
    }
}
