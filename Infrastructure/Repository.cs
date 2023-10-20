using Context;
using Microsoft.EntityFrameworkCore;
using ProdductApplication;

namespace Infrastructure
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
    {
        protected readonly DContext _context;//= new DContext();
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity?> GetALL()
        {
            return await _dbSet.FindAsync();
        }
    }
}