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
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get(){
            var produtos = _context.Produtos.AsNoTracking().ToList();

            if(produtos is null){
                return  NotFound();
            }
            return produtos;
        }

        [HttpGet("id:int", Name="ObeterProduto")]
        public ActionResult<Produto> Get(int id){
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p=>p.ProdutoId == id);

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

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObeterProduto", 
                new {id= produto.ProdutoId}, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produtos){
            if(id != produtos.ProdutoId){
                return BadRequest();
            }

            _context.Produtos.Entry(produtos).State = EntityState.Modified;
            _context.SaveChanges();
            
            return Ok(produtos);
        }
    
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id){
            var produtos = _context.Produtos.FirstOrDefault(p=>p.ProdutoId==id);

            if (produtos is null){
                return BadRequest();
            }

            _context.Produtos.Remove(produtos);
            _context.SaveChanges(); 

            return Ok(produtos);
        }    
    }
}
