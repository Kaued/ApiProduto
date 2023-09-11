using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogo.Repository;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _ufo;

        public CategoriasController(IUnitOfWork context)
        {
            _ufo = context;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Categoria>> Get(){
            var categorias = _ufo.CategoriaRepository.Get().ToList();

            if(categorias is null){
                return NoContent();
            }

            return categorias;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos(){
            var categorias = _ufo.CategoriaRepository.GetCategoriasProduto().ToList();
            if(categorias is null){
                return NoContent();
            }

            return categorias;
        }
        [HttpGet("{id:int}", Name = "ObeterCategoria")]
        public ActionResult<Categoria> Get(int id){
            var categorias = _ufo.CategoriaRepository.GetById(c => c.CategoriaId == id);

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
            _ufo.CategoriaRepository.Add(categoria);
            _ufo.Commit();

            return new CreatedAtRouteResult("ObeterCategoria", new {id = categoria.CategoriaId}, categoria);
        }

        [HttpPut("{id:int}")]

        public ActionResult<Categoria> Put(int id, Categoria categoria){
            if(id != categoria.CategoriaId){
                return BadRequest();
            }

            _ufo.CategoriaRepository.Update(categoria);
            _ufo.Commit();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id){
            var categoria = _ufo.CategoriaRepository.GetById(categoria => categoria.CategoriaId == id);
            if(categoria is null){
                return BadRequest();
            }

            _ufo.CategoriaRepository.Delete(categoria);
            _ufo.Commit();

            return Ok(categoria);
        }
    }
}
