using Myshop.Core.Models;
using MyShop.Core.Contractss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }
        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }
        public void Update(T t)
        {
            T updateT = items.Find(p => p.Id == t.Id);
            if (updateT != null)
            {
                updateT = t;
            }
            else
            {
                throw new Exception($"{className} not found");
            }
        }

        public T Find(string id)
        {
            T FindT = items.Find(p => p.Id == id);
            if (FindT != null)
            {
                return FindT;
            }
            else
            {
                throw new Exception($"{className} not found");
            }
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
        public void Delete(string id)
        {
            var deleteT = items.Find(p => p.Id == id);

            if (deleteT != null)
            {
                items.Remove(deleteT);
            }
            else
            {
                throw new Exception($"{className} not found");
            }
        }
    }
}
