using System.Linq.Expressions;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
  public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
  {
    
    public CategoriaRepository(AppDbContext context):base(context){

    }

        public IEnumerable<Categoria> GetCategoriasProduto()
        {
            return Get().Include(x => x.Produtos);
        }
    }
}