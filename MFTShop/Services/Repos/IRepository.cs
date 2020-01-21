using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MFTShop.Services.Repos
{
    interface IRepository<TModel>
    {
        #region GET
        TModel Get(int id);
        IQueryable<TModel> GetWhere(Expression<Func<TModel, bool>> predicate);
        IQueryable<TModel> GetWhere(Expression<Func<TModel, bool>> predicate,string UserId);
        IQueryable<TModel> GetWhere(Expression<Func<TModel, bool>> predicate, string UserId,bool GetDeleted=false);
        IQueryable<TModel> GetWhere(Expression<Func<TModel, bool>> predicate, bool GetDeleted = false);
        #endregion

        #region DELETE
        bool Delete(TModel model);
        bool Delete(int id);
        int DeleteAll(Expression<Func<TModel, bool>> predicate);
        int DeleteAll(Expression<Func<TModel, bool>> predicate,string UserId);
        int DeleteAll(Expression<Func<TModel, bool>> predicate, string UserId,bool GetDeleted=false);
        int DeleteAll(Expression<Func<TModel, bool>> predicate, bool GetDeleted = false);
        #endregion

        #region CREATE

        TModel Add(TModel model);
        TModel Add(TModel model,string UserId);
        ICollection<TModel> AddRange(ICollection<TModel> model);
        ICollection<TModel> AddRange(ICollection<TModel> model,string UserId);
        ICollection<TModel> AddRange(Expression<Func<TModel, bool>> predicate);
        ICollection<TModel> AddRange(Expression<Func<TModel, bool>> predicate, string UserId);
        
        #endregion





    }
}
