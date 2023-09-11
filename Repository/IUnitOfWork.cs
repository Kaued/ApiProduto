using System.Linq.Expressions;

namespace ApiCatalogo.Repository{

  public interface IUnitOfWork{
    
    IProdutoRepository ProdutoRepository{get;}

    ICategoriaRepository CategoriaRepository{get;}

    void Commit();  
    
  }
}