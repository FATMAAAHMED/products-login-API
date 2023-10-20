using System.Security.Cryptography;

namespace ProdductApplication
{
    public interface IRepository<TEntity, TId>
        where TEntity : class
    {
        Task<TEntity?> GetALL();

    

    }
}