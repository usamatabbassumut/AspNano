using AspNano.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.Specifications.Base
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            // modify the IQueryable using the specification's criteria expression
            if (specification.Condition != null)
            {
                query = query.Where(specification.Condition);
            }

            // Includes all expression-based includes
            if (specification.Includes.Count() > 0)
            {
                query = specification.Includes.Aggregate(query,
                                   (current, include) => current.Include(include));
            }

            // Include any string-based include statements
            if (specification.IncludeStrings.Count() > 0)
            {
                query = specification.IncludeStrings.Aggregate(query,
                                    (current, include) => current.Include(include));
            }
            // Apply ordering if expressions are set
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take);
            }
            return query;
        }
    }
}
