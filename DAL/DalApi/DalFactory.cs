﻿using DalApi;
using DalObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    class DalFactory
    {
        public static Idal GetDL(string type)
        {
            switch (type)
            {
                case object:
                    return DalObject.DalObject.Instance;
                default:
                    throw new DalException("Can't Get Dal Instance");
            }
        }

           
    }
}
