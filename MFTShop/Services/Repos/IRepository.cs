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


        bool Delete(TModel model);
        bool Delete(int id);
        int DeleteAll(Expression<Func<TModel, bool>> predicate);
        int DeleteAll(Expression<Func<TModel, bool>> predicate,string UserId);
        int DeleteAll(Expression<Func<TModel, bool>> predicate, string UserId,bool GetDeleted=false);
        int DeleteAll(Expression<Func<TModel, bool>> predicate, bool GetDeleted = false);








    }
}
