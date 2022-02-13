using DalApi;
using Singelton;
using System.Collections.Generic;
using System.Linq;
using static Dal.DataSource;

namespace Dal
{
    public sealed partial class DalObject : Singleton<DalObject>, Idal
    {
        /// <summary>
        /// DalObject constructor  call to intilialize
        /// </summary>
         DalObject()
        {
            Initialize(this);
        }

        static DalObject() { }

        /// <summary>
        /// Find if the id is exist in a spesific list
        /// </summary>
        /// <typeparam name="T">type of list</typeparam>
        /// <param name="lst">The list </param>
        /// <param name="id">The id for checking</param>
        public static bool ExistsIDCheck<T>(IEnumerable<T> list, int id)
        {
            if (!list.Any())
                return false;
            T temp = list.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id);

            return !(temp.Equals(default(T)));
        }
    }
    }

