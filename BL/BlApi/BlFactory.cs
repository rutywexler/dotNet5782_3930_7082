﻿using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public class BlFactory
    {
        public static IBL GetBL()
        {
            return Bl.BL.Instance;
        }
    }
}
