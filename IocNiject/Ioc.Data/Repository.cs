using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ioc.Core;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Ioc.Data
{
   public class Repository<T>:IRepository<T> where T:BaseEntity
    {
       private readonly IDbContext _context;

       private IDbSet<T> _entities;

       public Repository(IDbContext context)
       {
           this._context = context;
       }

       public T GetByID(object id)
       {
           return this.Entities.Find(id);
          
       }

       public void Insert(T entity)
       {
           try
           {
               if (entity == null)
               {
                   throw new ArgumentException("entity");
               }
               this.Entities.Add(entity);
               this._context.SaveChanges();
           }
           catch (DbEntityValidationException dbEx)
           {

               var msg = string.Empty;
               foreach (var validationErrors in dbEx.EntityValidationErrors)
               {
                   foreach (var validationError in validationErrors.ValidationErrors)
                   {
                       msg += string.Format("Property:{0} Error:{1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                   }
               }
               var fail = new Exception(msg, dbEx);
               throw fail;
           }
           
       }

       public void Update(T entity)
       {
           try
           {
               if (entity == null)
               {
                   throw new ArgumentException("entity");
               }
               this._context.SaveChanges();
           }
           catch (DbEntityValidationException dbEx)
           {
               var msg = string.Empty;
               foreach (var validationErrors in dbEx.EntityValidationErrors)
               {
                   foreach (var validationError in validationErrors.ValidationErrors)
                   {
                       msg += string.Format("Property:{0} Error:{1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                   }
               }
               var fail = new Exception(msg, dbEx);
               throw fail;
           }

           
       }

       public void Delete(T entity)
       {
           try
           {
               if (entity == null)
               {
                   throw new ArgumentException("entity");
               }
               this.Entities.Remove(entity);
               this._context.SaveChanges();
           }
           catch (DbEntityValidationException dbEx)
           {
               var msg = string.Empty;
               foreach (var validationErrors in dbEx.EntityValidationErrors)
               {
                   foreach (var validationError in validationErrors.ValidationErrors)
                   {
                       msg += string.Format("Property:{0} Error:{1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                   }
               }
               var fail = new Exception(msg, dbEx);
               throw fail;
           }
           //throw new NotImplementedException();
       }

       public IQueryable<T> Table
       {
           get 
           {
               return this.Entities;
           }
       }

       private IDbSet<T> Entities
       {
           get 
           {
               if (_entities == null)
               {
                   _entities = _context.Set<T>();
               }
               return _entities;
           }
       }
    }
}
