using System.Linq.Expressions;
using APICatalogo.Models;

namespace ApiCatalogo.Repository{

  public interface ICategoriaRepository: IRepository<Categoria>{

    IEnumerable<Categoria> GetCategoriasProduto();

  }
}