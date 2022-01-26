using AspNano.Application.Specifications.Base;
using AspNano.Core.Interfaces;
using AspNano.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.Repositories.Base
{
    public class EFRepository<T, Tkey> : IAsyncRepository<T, Tkey> where T : class
    {
        private readonly ApplicationDbContext db;
    

        public EFRepository(ApplicationDbContext context)
        {
            db = context;

        }

        public async Task<T> AddAsync(T entity)
        {
            await db.Set<T>().AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(IKey Id)
        {
            return await db.Set<T>().FindAsync(Id)
;
        }

        public async Task UpdateAsync(T entity)
        {
            db.Entry(entity);
            await db.SaveChangesAsync();
        }
        public async Task<List<T>> GetAllAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(db.Set<T>().AsQueryable(), spec);
        }
    }
}
