using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;
using static DalObject.DataSource.Config;

namespace DalObject
{
    public partial class DalObject:IDAL.IDal
    {
        /// <summary>
        /// DalObject constructor  call to intilialize
        /// </summary>
        public DalObject()
        {
            Initalize();  
        }


 
        /// <summary>
        /// Find if the id is exist in a spesific list
        /// </summary>
        /// <typeparam name="T">type of list</typeparam>
        /// <param name="lst">The list </param>
        /// <param name="id">The id for checking</param>
        static bool ExistsIDCheck<T>(IEnumerable<T> list, int id)
        {
            if (!list.Any())
                return false;
            T temp = list.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id);

            return !(temp.Equals(default(T)));
        }





    }


    }

