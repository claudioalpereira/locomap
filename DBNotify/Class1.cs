using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBNotify
{
    public class Class1
    {
        public static void Subscribe(string connString, string query, Action callback)
        {
           
        }

        public static ObjectQuery GetObjectQuery<TEntity>(DbContext context, IQueryable query)
    where TEntity : class
        {
            if (query is ObjectQuery)
                return query as ObjectQuery;


            if (context == null)
                throw new ArgumentException("Paramter cannot be null", "context");


            // Use the DbContext to create the ObjectContext 
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            // Use the DbSet to create the ObjectSet and get the appropriate provider. 
            IQueryable iqueryable = objectContext.CreateObjectSet<TEntity>() as IQueryable;
            IQueryProvider provider = iqueryable.Provider;


            // Use the provider and expression to create the ObjectQuery. 
            return provider.CreateQuery(query.Expression) as ObjectQuery;
        }
    }
}
