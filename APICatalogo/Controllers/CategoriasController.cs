using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;

        public CategoriasController(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        //[HttpGet("produtos")]
        //public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        //{
        //    return _context.Categorias.Include(p => p.Produtos).ToList();
        //}

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategoria()
        {
            return Ok(_repository.GetCategorias());
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetCategoriasById(int id)
        {
            var categoria = _repository.GetCategoria(id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrado");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Dados inválidos");
            }

            var categoriaCreate = _repository.Create(categoria);
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCreate.CategoriaId }, categoriaCreate);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Dados inválidos");
            }

            _repository.Update(categoria);
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _repository.GetCategoria(id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrado");
            }

            var categoriaDelete = _repository.Delete(id);
            return Ok(categoriaDelete);
        }
    }
}
