using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogo.Repository;
using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ApiLogginFilter))]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(IUnitOfWork context, ILogger<ProdutosController> logger)
        {
            _uof = context;
            _logger = logger;
        }

        [HttpGet("menorPreco")]
        public ActionResult<IEnumerable<Produto>> GetOrderPreco(){
            return _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get(){
            var produtos = _uof.ProdutoRepository.Get().ToList();

            _logger.LogInformation("<=============Get api/produtos==========>");
            if(produtos is null){
                return  NotFound();
            }
            return produtos;
        }

        [HttpGet("id:int", Name="ObeterProduto")]
        public ActionResult<Produto> Get(int id){
            var produto = _uof.ProdutoRepository.GetById(p=>p.ProdutoId == id);

            if(produto == null){
                return NotFound();
            }

            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto){
            if(produto is null){
                return BadRequest(); 
            } 

            _uof.ProdutoRepository.Add(produto);
            _uof.Commit();

            return new CreatedAtRouteResult("ObeterProduto", 
                new {id= produto.ProdutoId}, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produtos){
            if(id != produtos.ProdutoId){
                return BadRequest();
            }

            _uof.ProdutoRepository.Update(produtos);
            _uof.Commit();
            
            return Ok(produtos);
        }
    
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id){
            var produtos = _uof.ProdutoRepository.GetById(p=>p.ProdutoId==id);

            if (produtos is null){
                return BadRequest();
            }

            _uof.ProdutoRepository.Delete(produtos);
            _uof.Commit(); 

            return Ok(produtos);
        }    
    }
}
