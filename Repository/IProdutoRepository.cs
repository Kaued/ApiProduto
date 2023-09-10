using System.Linq.Expressions;
using APICatalogo.Models;

namespace ApiCatalogo.Repository{

  public interface IProdutoRepository: IRepository<Produto>{

    IEnumerable<Produto> GetProdutosPorPreco();

  }
}