using System.Linq.Expressions;
using APICatalogo.Context;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
  public class Repository<T> : IRepository<T> where T : class
  {
    protected AppDbContext _context;

    public Repository(AppDbContext context){
      this._context = context;
    }
    public void Add(T entity)
    {
      _context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
      _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> Get()
    {
      return _context.Set<T>().AsNoTracking();
    }

    public T GetById(Expression<Func<T, bool>> predicate)
    {
      return _context.Set<T>().SingleOrDefault(predicate);
    }

    public void Update(T entity)
    { 
      _context.Entry(entity).State = EntityState.Modified;
      _context.Set<T>().Update(entity);
    }
  }
}