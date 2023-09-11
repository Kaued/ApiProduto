using System.Linq.Expressions;
using APICatalogo.Context;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
  public class UnitOfWork : IUnitOfWork
  {
    protected AppDbContext _context;
    private ProdutoRepository _produtoRepo;
    private CategoriaRepository _categoriaRepo;
    public UnitOfWork(AppDbContext context)
    {
      this._context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
      get{
        return _produtoRepo= _produtoRepo ?? new ProdutoRepository(_context);
      }
    }

    public ICategoriaRepository CategoriaRepository
    {
      get{
        return _categoriaRepo= _categoriaRepo ?? new CategoriaRepository(_context);
      }
    }

    public void Commit()
    {
      _context.SaveChanges();
    }

    public void Dispose(){
      _context.Dispose();
    }
  }
}