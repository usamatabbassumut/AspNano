using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Core.Interfaces
{
    public interface IAsyncRepository<T, Tkey> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(IKey Id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(ISpecification<T> specification);

    }
}
