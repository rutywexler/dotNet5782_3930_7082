using DAL.DalApi;
using Dal;
using System;
using System.IO;
using System.Reflection;

namespace DalApi
{
    public static class DalFactory
    {
        public static Idal GetDL()
        {
            Assembly.LoadFrom($@"{Directory.GetCurrentDirectory()}\{DalConfig.DalType}.dll");
            Type type = Type.GetType($"{DalConfig.Namespace}.{DalConfig.DalType}, {DalConfig.DalType}");
            if (type == null)
                throw new DAL.DalApi.DalConfigException("Can't find such project");
            Idal dal = (Idal)type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).GetValue(null);
            if (dal == null)
                throw new DAL.DalApi.DalConfigException("Can't Get Dal Instance");
            return dal;
        }
    }
}
