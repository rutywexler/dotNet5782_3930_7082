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
        /// DalObject constructor call to intilialize
        /// </summary>
        public DalObject()
        {
            Initalize();  
        }
     
        
        /// <summary>
        /// Valid is a static method in the DalObject class.
        /// the helper method check if input is valid 
        /// </summary>
        /// <param name="input">out int value</param>
        public static void Valid(out int input)
        {
            bool parse = int.TryParse(Console.ReadLine(), out input);
            while (!parse)
            {
                Console.WriteLine("not valid input! please enter again");
                parse = int.TryParse(Console.ReadLine(), out input);
            }
        }
        /// <summary>
        /// Valid2 is a static method in the DalObject class.
        /// the helper method check if input is valid in range 
        /// </summary>
        /// <param name="min">the first int value</param>
        /// <param name="max">second int vaalue</param>
        /// <param name="input">3th bout int value</param>
        public static void ValidRange(int min, int max, out int input)
        {
            bool parse = int.TryParse(Console.ReadLine(), out input);
            while (!parse || input > max || input < min)
            {
                Console.WriteLine("not valid input! please enter again");
                parse = int.TryParse(Console.ReadLine(), out input);
            }
        }
       
        /// <summary>
        /// checkValid is a static method in the DalObject class.
        /// the helper method check if input is valid if not the func throws exception 
        /// </summary>
        /// <param name="id">the first int value</param>
        /// <param name="min">the second int value</param>
        /// <param name="max">3th out int value</param>
        public void checkValid(int id, int min, int max)
        {
            if (id < min || id >= max)
            {
                throw new FormatException();
            }
        }

        
       
        
    }


}

