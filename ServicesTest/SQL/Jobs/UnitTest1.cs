using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Hit.Services.DataAccess.DT.Sql;
using Hit.Services.Helpers.Classes.Classes;
using NUnit.Framework;

namespace HitServicesTest //  <----<<< IMPORTANT: All tests must have that namespace
{
    [TestFixture]
    public class UnitTest1
    {

        ConfigHelper ch;

        [OneTimeSetUp]
        public void Init()
        {
            ch = new ConfigHelper();
        }

        //[Test, Order(1)]
        //public void TestMethod1()
        //{
        //    //RunSQLScriptsDT dt = new RunSQLScriptsDT();
        //    //string select = "SELECT TOP (10) [kdnr],[name1],[name2] FROM [protel].[proteluser].[kunden]";
        //    //string conStr = ch.ConnectionString("sisifos", "protel", "sqladmin", "111111");
        //    //IEnumerable<dynamic> enumerable = dt.Select(conStr, select);
        //    //List<class1> c1 = Mapper.Map<List<class1>>(enumerable);// <--- use AutoMapper
        //    //List<IDictionary<string, dynamic>> result1 = Mapper.Map<List<IDictionary<string, dynamic>>>(enumerable);
        //    //List<IDictionary<string, dynamic>> result = dt.ToDictionary(enumerable);//list.ConvertAll(
        //    //                                                                        //  new Converter<dynamic, Dictionary<string, dynamic>>(DynamicToDictionaryConverter));
        //                                                                            //   IEnumerable<IDictionary<string, dynamic>> dict = ((IEnumerable<IDictionary<string, dynamic>>)list);// (list as IEnumerable<IDictionary<string, dynamic>>);//.ToList<IDictionary<string, dynamic>>();

        //}


        //public static Dictionary<string, dynamic> DynamicToDictionaryConverter(dynamic item)
        //{
        //    if (IsDictionary(item))
        //    {
        //        return (Dictionary<string, dynamic>)item;
        //    }

        //    Dictionary<string, dynamic> newItem = new Dictionary<string, dynamic>();
        //    PropertyInfo[] props = item.GetType().GetProperties();
        //    foreach (PropertyInfo prop in props)
        //    {
        //        newItem[prop.Name] = item.GetType().GetProperty(prop.Name).GetValue(item, null);
        //    }
        //    return newItem;
        //}

        //public static bool IsDictionary(object o)
        //{
        //    if (o == null) return false;
        //    return o is IDictionary &&
        //           o.GetType().IsGenericType &&
        //           o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        //}
    }

    public class class1{

        public int kdnr { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }

      
    }

}
