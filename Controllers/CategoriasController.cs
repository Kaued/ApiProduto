using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get(){
            var categorias = _context.Categorias.AsNoTracking().ToList();

            if(categorias is null){
                return NoContent();
            }

            return categorias;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos(){
            var categorias = _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToList();
            if(categorias is null){
                return NoContent();
            }

            return categorias;
        }
        [HttpGet("{id:int}", Name = "ObeterCategoria")]
        public ActionResult<Categoria> Get(int id){
            var categorias = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);

            if(categorias is null){
                return NoContent();
            }

            return categorias;
        }

        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria){

            if(categoria is null){
                return BadRequest();
            }	
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObeterCategoria", new {id = categoria.CategoriaId}, categoria);
        }

        [HttpPut("{id:int}")]

        public ActionResult<Categoria> Put(int id, Categoria categoria){
            if(id != categoria.CategoriaId){
                return BadRequest();
            }

            _context.Categorias.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id){
            var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.CategoriaId == id);
            if(categoria is null){
                return BadRequest();
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}
