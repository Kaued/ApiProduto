using System.Linq.Expressions;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
  public class ProdutoRepository : Repository<Produto>, IProdutoRepository
  {
    
    public ProdutoRepository(AppDbContext context):base(context){

    }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(c =>c.Preco).ToList();
        }
    }
}