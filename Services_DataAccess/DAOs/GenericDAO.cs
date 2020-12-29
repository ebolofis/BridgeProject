using AutoMapper;
using Dapper;
using Hit.Services.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.DataAccess.DAOs
{
    /// <summary>
    /// Generic DAO class for simple CRUD operations
    /// </summary>
    /// <typeparam name="T">DTO classes ONLY</typeparam>
    public class GenericDAO<T>  where T : class
    {
        

        /// <summary>
        /// Execute a parameterized query (insert or update)
        /// </summary>
        /// <param name="db">DB connection</param>
        /// <param name="sql">Insert or Update query.  ex: "UPDATE Author SET FirstName = @FirstName, LastName = @LastName " + "WHERE Id = @Id";</param>
        /// <param name="parameters">parameters (optional). Parameters must match query parameters. ex: new {FirstName="Smith",Lastname="Tom", Id=30 } </param>
        /// <returns>the number of affected rows</returns>
        public int Execute(IDbConnection db, string sql, object parameters = null)
        {
            return db.Execute(sql, parameters);
        }

        /// <summary>
        /// Select table's Data from the Source DB
        /// </summary>
        /// <typeparam name="T">DTO class</typeparam>
        /// <param name="table">the table to select from. Table must be the name of T class</param>
        /// /// <param name="db">DB connection</param>
        /// <returns>the list of Data (the entire table)</returns>
        public List<T> Select(IDbConnection db, string table)
        {
            string select = "Select * From [" + table + "]";
            return db.Query<T>(select).ToList();
        }


        /// <summary>
        /// Select table's Data from the Source DB
        /// </summary>
        /// <typeparam name="T">DTO class</typeparam>
        /// <param name="sql">the sql select statement. ex: "Select * From Author WHERE  age = @Age or Name like @Name"</param>
        /// <param name="Parameters">Where and/or orderBy parameters (optional). Parameters must match whereConditions and/or OrderBy Conditions. ex: new {Age = 10, Name = "Tom%"}</param>
        /// <param name="db">DB connection</param>
        /// <returns>the list of Data</returns>
        public List<T> Select(string sql, object Parameters, IDbConnection db)
        {
            return db.Query<T>(sql, Parameters).ToList();
        }

        /// <summary>
        /// Select data from the Source DB as List of Dictionary(key:string, value: dynamic)
        /// </summary>
        /// <typeparam name="T">DTO class</typeparam>
        /// <param name="sql">the sql select statement. ex: "Select * From Author WHERE  age = @Age or Name like @Name"</param>
        /// <param name="Parameters">Where and/or orderBy parameters (optional). Parameters must match whereConditions and/or OrderBy Conditions. ex: new {Age = 10, Name = "Tom%"}</param>
        /// <param name="db">DB connection</param>
        /// <returns>the list of Data</returns>
        public List<IDictionary<string, dynamic>> SelectDictionaries(string sql, object Parameters, IDbConnection db)
        {
            return (db.Query(sql, Parameters) as IEnumerable<IDictionary<string, dynamic>>).ToList<IDictionary<string, dynamic>>();
        }

        /// <summary>
        /// Select the First Object from the Source DB
        /// </summary>
        /// <typeparam name="T">DTO class</typeparam>
        /// <param name="sql">the sql select statement. ex: "Select * From Author WHERE  age = @Age or Name like @Name"</param>
        /// <param name="whereParameters">where parameters (optional). Parameters must match whereConditions. ex: new {Age = 10, Name = "Tom%"}</param>
        /// <param name="db">DB connection</param>
        /// <returns>an Object or null</returns>
        public T SelectFirst(string sql, object whereParameters, IDbConnection db)
        {
            return db.Query<T>(sql, whereParameters).FirstOrDefault<T>();
        }

        /// <summary>
        /// Select one object based on id (long)
        /// </summary>
        /// <param name="db">DB connection</param>
        /// <param name="Id">long Id, DB's key</param>
        /// <returns></returns>
        public T Select(IDbConnection db, long Id)
        {
            return db.Get<T>(Id);
        }

        /// <summary>
        /// Select one object based on id (int)
        /// </summary>
        /// <param name="db">DB connection</param>
        /// <param name="Id">int Id, DB's key</param>
        /// <returns></returns>
        public T Select(IDbConnection db, int Id)
        {
            return db.Get<T>(Id);
        }


        /// <summary>
        /// Select table's Data from the Source DB
        /// </summary>
        /// <typeparam name="T">DTO class</typeparam>
        /// <param name="db">DB connection</param>
        /// <returns>the list of Data (the entire table)</returns>
        public List<T> Select(IDbConnection db)
        {
            return db.GetList<T>().ToList();
        }


        /// <summary>
        /// Select table's Data using where statetment
        /// </summary>
        /// <typeparam name="T">DTO class</typeparam>
        /// <param name="whereConditions">where conditions. ex: "where age = @Age or Name like @Name"</param>
        /// <param name="whereParameters">where parameters (optional). Parameters must match whereConditions. ex: new {Age = 10, Name = "Tom%"}</param>
        /// <param name="db">DB connection</param>
        /// <returns>the list of Data (the entire table)</returns>
        public List<T> Select(IDbConnection db, string whereConditions, object whereParameters)
        {
            return db.GetList<T>(whereConditions, whereParameters).ToList();
        }

        /// <summary>
        /// Select the first object (or null) using where statetment
        /// </summary>
        /// <typeparam name="T">DTO class</typeparam>
        /// <param name="whereConditions">where conditions. ex: "where age = @Age or Name like @Name"</param>
        /// <param name="whereParameters">where parameters (optional). Parameters must match whereConditions. ex: new {Age = 10, Name = "Tom%"}</param>
        /// <param name="db">DB connection</param>
        /// <returns>the first object or null</returns>
        public T SelectFirst(IDbConnection db, string whereConditions, object whereParameters)
        {
            return db.GetList<T>(whereConditions, whereParameters).FirstOrDefault<T>();
        }

        /// <summary>
        /// Return the results of a select query as IEnumerable(dynamic). Use AutoMapper to convert dynamic to specific model
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> SelectEnumerable(IDbConnection db, string sql)
        {
            return db.Query(sql);
        }

        /// <summary>
        /// run an sql script (containing a select statement) and return data as List of Dictionary(key:string, value: dynamic)>.
        /// 
        /// </summary>
        /// <notes>
        ///    List(IDictionary(string, dynamic)): 
        ///        Every item in the list is a DB's row.
        ///        Every DB's row is represented as Dictionary(string, dynamic), where key is column name.
        /// </notes>
        /// <param name="sql">select query</param>
        /// <param name="db"></param>
        /// <returns>Data as key-value pairs</returns>
        public IEnumerable<dynamic> SelectDictionary(IDbConnection db, string sql)
        {
            return (db.Query(sql) as IEnumerable<IDictionary<string, dynamic>>).ToList<IDictionary<string, dynamic>>();
        }

        /// <summary>
        /// Convert IEnumerable(dynamic) to List(IDictionary(string, dynamic)). (see SelectEnumerable)
        /// </summary>
        /// <param name="list">IEnumerable(dynamic)</param>
        /// <returns></returns>
        public List<IDictionary<string, dynamic>> ToDictionary(IEnumerable<dynamic> list)
        {
            return (list as IEnumerable<IDictionary<string, dynamic>>).ToList<IDictionary<string, dynamic>>();
        }


        /// <summary>
        /// Convert a dynamic IEnumerable to a Model's List (see SelectEnumerable)
        /// </summary>
        /// <typeparam name="T">a Model</typeparam>
        /// <param name="list">dynamic list</param>
        /// <returns></returns>
        public List<T> ToModelList<T>(IEnumerable<dynamic> list)
        {
            return Mapper.Map<List<T>>(list); 
        }


        /// <summary>
        /// Select a List of long from the Source DB
        /// </summary>
        /// <param name="db">DB connection</param>
        /// <param name="sql">the sql select statement (must return only a list of longs). ex: "Select CompanyId From HESUserCompanyAssoc WHERE  UserId = @UserId"</param>
        /// <param name="Parameters">Where and/or orderBy parameters (optional). Parameters must match whereConditions and/or OrderBy Conditions. ex: new {UserId = 1}</param>
        /// <returns>the list of Data</returns>
        public List<long> QueryLong(IDbConnection db, string sql, object whereParameters)
        {
            return db.Query<long>(sql, whereParameters).ToList();
        }

        /// <summary>
        /// Select a List of int from the Source DB
        /// </summary>
        /// <param name="db">DB connection</param>
        /// <param name="sql">the sql select statement (must return only a list of int). ex: "Select CompanyId From HESUserCompanyAssoc WHERE  UserId = @UserId"</param>
        /// <param name="Parameters">Where and/or orderBy parameters (optional). Parameters must match whereConditions and/or OrderBy Conditions. ex: new {UserId = 1}</param>
        /// <returns>the list of Data</returns>
        public List<int> QueryInt(IDbConnection db, string sql, object whereParameters)
        {
            return db.Query<int>(sql, whereParameters).ToList();
        }


        /// <summary>
        /// Select a List of string from the Source DB
        /// </summary>
        /// <param name="db">DB connection</param>
        /// <param name="sql">the sql select statement (must return only a list of string). ex: "Select Code From HESUser WHERE  isActive = @Active"</param>
        /// <param name="Parameters">Where and/or orderBy parameters (optional). Parameters must match whereConditions and/or OrderBy Conditions. ex: new {Active = true}</param>
        /// <returns>the list of Data</returns>
        public List<string> QueryString(IDbConnection db, string sql, object whereParameters)
        {
            return db.Query<string>(sql, whereParameters).ToList();
        }


        /// <summary>
        /// Insert a new record to DB. Return the new Id (primary key as long)
        /// </summary>
        /// <typeparam name="T">DTO class</typeparam>
        /// <param name="item">the item to insert </param>
        /// <param name="db">DB connection</param>
        /// <returns>Return the new Id (primary key)</returns>
        public long Insertlong(IDbConnection db, T item)
        {
            return db.Insert<long>(item);
        }

        /// <summary>
        /// Insert a new record to DB. Return the new Id (primary key as int)
        /// </summary>
        /// <typeparam name="T">DTO class</typeparam>
        /// <param name="item">the item to insert </param>
        /// <param name="db">DB connection</param>
        /// <returns>Return the new Id (primary key)</returns>
        public int InsertInt(IDbConnection db, T item)
        {
            return db.Insert<int>(item);
        }


        /// <summary>
        /// Update a record to DB matched by Id. Return the number of raws affected.
        /// If no row found return 0.
        /// </summary>
        /// <typeparam name="T">DTO class</typeparam>
        /// <param name="item">the item to insert </param>
        /// <param name="db">DB connection</param>
        /// <returns>Return the number of raws affected</returns>
        public int Update(IDbConnection db, T item)
        {
            return db.Update(item);
        }

        /// <summary>
        /// Update a  list of DTO objects (T) to DB matched by Id. Return the number of raws affected
        /// </summary>
        /// <typeparam name="T">DTO class</typeparam>
        /// <param name="list">the list of items to insert </param>
        /// <param name="db">DB connection</param>
        /// <returns>Return the number of raws affected</returns>
        public int UpdateList(IDbConnection db, List<T> list)
        {
            int rowsAffected = 0;
            foreach (T item in list)
            {
                rowsAffected = rowsAffected + db.Update(item);
            }
            return rowsAffected;
        }

        /// <summary>
        /// Delete a record from DB matched by Id. Return the number of raws affected
        /// </summary>
        /// <param name="Id">the Id to delete</param>
        /// <param name="db">DB connection</param>
        /// <returns>Return the number of raws affected</returns>
        public int Delete(IDbConnection db, long Id)
        {
            return db.Delete<T>(Id);
        }



        /// <summary>
        /// Delete a record from DB matched by Id. Return the number of raws affected
        /// </summary>
        /// <param name="item">the item to delete (only Id is required) </param>
        /// <param name="db">DB connection</param>
        /// <returns>Return the number of raws affected</returns>
        public int Delete(IDbConnection db, T item)
        {
            return db.Delete(item);
        }

        /// <summary>
        /// Delete multiple records using where clause
        /// </summary>
        /// <param name="db">DB connection</param>
        /// <param name="whereConditions">where conditions. ex: "where age = @Age or Name like @Name"</param>
        /// <param name="whereConditions">where parameters (optional). Parameters must match whereConditions. ex: new {Age = 10, Name = "Tom%"}</param>
        /// <returns>Return the number of raws affected</returns>
        public int DeleteList(IDbConnection db, string whereConditions, object whereParameters = null)
        {
            return db.DeleteList<T>(whereConditions, whereParameters);
        }

        /// <summary>
        /// Delete multiple records using List of Ids
        /// </summary>
        /// <param name="Id">the Id's list to delete</param>
        /// <param name="db">DB connection</param>
        /// <returns>Return the number of raws affected</returns>
        public int DeleteList(IDbConnection db, List<long> Ids)
        {
            int c = 0;
            foreach (long id in Ids)
            {
                c = c + db.Delete<T>(id);
            }
            return c;
        }


        /// <summary>
        /// Get count of records 
        /// </summary>
        /// <param name="db">DB connection</param>
        /// <param name="whereConditions">where conditions. ex: "where age = @Age or Name like @Name"</param>
        /// <param name="whereConditions">where parameters (optional). Parameters must match whereConditions. ex: new {Age = 10, Name = "Tom%"}</param>
        /// <returns>Return the number of raws affected</returns>
        public int RecordCount(IDbConnection db, string whereConditions, object whereParameters = null)
        {
            return db.RecordCount<T>(whereConditions, whereParameters);
        }

        /// <summary>
        /// Pagenation
        /// </summary>
        /// <param name="db">DB connection</param>
        /// <param name="pageNumber">page Number</param>
        /// <param name="rowsPerPage">rows Per Page</param>
        /// <param name="conditions">where conditions. ex: "where age = @Age or Name like @Name"</param>
        /// <param name="orderBy">order by conditions ex: "Description desc"</param>
        /// <param name="parameters">where parameters (optional). Parameters must match whereConditions. ex: new {Age = 10, Name = "Tom%"}</param>
        /// <returns>the list of items for the page</returns>
        public virtual PaginationModel<T> GetPage(IDbConnection db, int pageNumber, int rowsPerPage, string conditions, string orderBy, object parameters = null)
        {
            PaginationModel<T> pagination = new PaginationModel<T>();
            if (pageNumber > 0)
            {
                pagination.PageList = db.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderBy, parameters).ToList<T>();
                pagination.CurrentPage = pageNumber;
                pagination.ItemsCount = RecordCount(db, conditions, parameters);
                pagination.PageLength = pagination.PageList.Count();
                pagination.PagesCount = getNumberOfPages(pagination.ItemsCount, rowsPerPage);
            }
            else
            {
                pagination.PageList = db.GetList<T>(parameters).ToList();
                pagination.ItemsCount = pagination.PageList.Count;
                pagination.CurrentPage = 0;
                pagination.PageLength = pagination.ItemsCount;
                pagination.PagesCount = 0;
            }
            return pagination;
        }






        /// <summary>
        /// Custom Pagination. Return a PaginationModel where T is a Model (typically no DTO). 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sqlData">sql query returning data. For paging it requires parameters: @StartRow and @EndRow</param>
        /// <param name="sWhere">Where for SQLData statment</param>
        /// <param name="sqlCount">sql query returning the total num of records (return one int ONLY). Required inly if pageNumber > 0</param>
        /// <param name="pageNumber">page Number</param>
        /// <param name="rowsPerPage">rows Per Page</param>
        /// <param name="companyId">companyId (optional, 0 for every company)</param>
        /// <example>
        ///      string sqlData = @"SELECT *
        ///            FROM(
        ///             SELECT ROW_NUMBER() OVER(ORDER BY hc.Id) rowId, hc.*, 
        ///
        ///                    ISNULL(hr.Descr,'') Region,  ISNULL(h.Descr,'') City, ISNULL(hd.Descr,'') District, ISNULL(hg.Descr,'') GeographicalAreaCode, ISNULL(hcc.Descr,'') Country
        ///                     FROM HESPostCode AS hc
        ///                     LEFT OUTER JOIN HESRegion AS hr ON hr.Id = hc.RegionId
        ///                     LEFT OUTER JOIN HESCity AS h ON h.Id = hc.CityId
        ///                     LEFT OUTER JOIN HESDistrict AS hd ON hd.Id = hc.DistrictId
        ///                     LEFT OUTER JOIN HESGeographicArea AS hg ON hg.Id = hc.GeographicalAreaCodeId
        ///                     LEFT OUTER JOIN HESCountry AS hcc ON hcc.Id = hc.CountryId
        ///                     ) fin";
        ///         string sWhere = "WHERE fin.rowId BETWEEN @StartRow AND @EndRow";
        ///
        ///      string sqlCount = @"SELECT COUNT(*)  FROM HESPostCode AS hc";
        /// 
        /// </example>
        /// <returns>a List of Models (typically no DTO)</returns>
        public virtual PaginationModel<T> GetPage(IDbConnection db, string sqlData, string sWhere, string sqlCount, int pageNumber, int rowsPerPage, long companyId = 0)
        {
            PaginationModel<T> pagination = new PaginationModel<T>();
            if (pageNumber > 0)
            {
                using (var multipleresult = db.QueryMultiple(sqlData + " " + sWhere + ";" + sqlCount, new { StartRow = ((pageNumber - 1) * rowsPerPage) + 1, EndRow = ((pageNumber - 1) * rowsPerPage) + rowsPerPage, CompanyId = companyId }))
                {
                    pagination.PageList = multipleresult.Read<T>().ToList();
                    pagination.ItemsCount = multipleresult.Read<int>().FirstOrDefault<int>();
                }

                pagination.CurrentPage = pageNumber;
                pagination.PageLength = pagination.PageList.Count();
                pagination.PagesCount = getNumberOfPages(pagination.ItemsCount, rowsPerPage);
            }
            else
            {
                if (companyId == 0)
                    pagination.PageList = Select(sqlData, null, db);
                else
                    pagination.PageList = Select(sqlData, new { CompanyId = companyId }, db);
                pagination.ItemsCount = pagination.PageList.Count;
                pagination.CurrentPage = 0;
                pagination.PageLength = pagination.ItemsCount;
                pagination.PagesCount = 0;
            }
            return pagination;
        }

        /// <summary>
        /// Custom Selection. Return a List of T where T is a Model (typically no DTO) using sqlData as command string. 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sqlData"></param>
        /// <example>
        ///             SELECT hc.*, 
        ///
        ///                    ISNULL(hr.Descr,'') Region,  ISNULL(h.Descr,'') City, ISNULL(hd.Descr,'') District, ISNULL(hg.Descr,'') GeographicalAreaCode, ISNULL(hcc.Descr,'') Country
        ///                     FROM HESPostCode AS hc
        ///                     LEFT OUTER JOIN HESRegion AS hr ON hr.Id = hc.RegionId
        ///                     LEFT OUTER JOIN HESCity AS h ON h.Id = hc.CityId
        ///                     LEFT OUTER JOIN HESDistrict AS hd ON hd.Id = hc.DistrictId
        ///                     LEFT OUTER JOIN HESGeographicArea AS hg ON hg.Id = hc.GeographicalAreaCodeId
        ///                     LEFT OUTER JOIN HESCountry AS hcc ON hcc.Id = hc.CountryId
        ///             WHERE hc.Code = '13322'
        /// </example>
        /// <returns>a List of Models (typically no DTO)</returns>
        public List<T> GetExtendedModels(IDbConnection db, string sqlData, object param = null)
        {
            return db.Query<T>(sqlData, param).ToList<T>();
        }

        /// <summary>
        /// Return 2 select statetments unrelated each other. Return a tuple object
        /// </summary>
        /// <typeparam name="U">DTO or Model</typeparam>
        /// <param name="db">DB connection</param>
        /// <param name="sql1">1st select statetment. Results returned as List<T> into the Tuple.  ex: Select * From Product </param>
        /// <param name="sql2">2nd select statetment. Results returned as List<U> into the Tuple.  ex: select * from Actions</param>
        /// <param name="parameters">where parameters (optional). Parameters must match whereConditions. ex: new {Age = 10, Name = "Tom%"}</param>
        /// <returns>Return a tuple object</returns>
        public Tuple<List<T>, List<U>> Select2Queries<U>(IDbConnection db, string sql1, string sql2, object parameters = null) where U : class
        {
            Tuple<List<T>, List<U>> tuple;
            string sql = sql1 + ";" + sql2;
            using (var multipleresult = db.QueryMultiple(sql, parameters))
            {
                var t = multipleresult.Read<T>().ToList();
                var u = multipleresult.Read<U>().ToList();
                tuple = new Tuple<List<T>, List<U>>(t, u);
            }
            return tuple;
        }


        /// <summary>
        /// Return 3 select statetments unrelated each other. Return a tuple object
        /// </summary>
        /// <typeparam name="U">DTO or Model</typeparam>
        /// <param name="db">DB connection</param>
        /// <param name="sql1">1st select statetment. Results returned as List<T> into the Tuple.  ex: Select * From Product </param>
        /// <param name="sql2">2nd select statetment. Results returned as List<U> into the Tuple.  ex: select * from Actions</param>
        /// <param name="sql3">3nd select statetment. Results returned as List<W> into the Tuple.  ex: select * from Discount</param>
        /// <param name="parameters">where parameters (optional). Parameters must match whereConditions. ex: new {Age = 10, Name = "Tom%"}</param>
        /// <returns>Return a tuple object</returns>
        public Tuple<List<T>, List<U>, List<W>> Select3Queries<U, W>(IDbConnection db, string sql1, string sql2, string sql3, object parameters = null) where U : class where W : class
        {
            Tuple<List<T>, List<U>, List<W>> tuple;
            string sql = sql1 + ";" + sql2 + ";" + sql3;
            using (var multipleresult = db.QueryMultiple(sql, parameters))
            {
                var t = multipleresult.Read<T>().ToList();
                var u = multipleresult.Read<U>().ToList();
                var w = multipleresult.Read<W>().ToList();
                tuple = new Tuple<List<T>, List<U>, List<W>>(t, u, w);
            }
            return tuple;
        }

        /// <summary>
        /// Return the max Id for a table into the DB. If no record found then return 0
        /// </summary>
        /// <param name="db">DB connection</param>
        /// <param name="table">the table name</param>
        /// <returns></returns>
        public long GetMaxId(IDbConnection db, string table)
        {
            string select = "Select isnull(max(Id),0) From [" + table + "]";
            return db.Query<long>(select).First<long>();
        }

        /// <summary>
        /// Disable Id auto increment
        /// </summary>
        /// <param name="db"></param>
        public void IdentityInsertOn(IDbConnection db)
        {
            db.Execute("SET IDENTITY_INSERT [" + getTableName() + "] ON"); //disable identity
        }

        //Enable Id auto increment
        public void IdentityInsertOff(IDbConnection db)
        {
            db.Execute("SET IDENTITY_INSERT [" + getTableName() + "] OFF"); //enable identity
        }

        /// <summary>
        /// On pagination, return the number of pages
        /// </summary>
        /// <param name="totalCount">the total number oc rows</param>
        /// <param name="pageSize">the page size</param>
        /// <returns></returns>
        protected int getNumberOfPages(int totalCount, int pageSize)
        {
            int pageNum = 0;
            if (pageSize > 0)
            {
                pageNum = totalCount / pageSize;
                if (pageNum % pageSize > 0) pageNum++;
            }
            return pageNum;
        }




        /// <summary>
        /// Return Table Name based DTO's attribute TableAttribute or DTO's class name
        /// </summary>
        /// <returns></returns>
        public string getTableName()
        {
            Type cl = typeof(T);//.Assembly.GetType(typeof(U).Name);
            var classCustomAttributes = (Dapper.TableAttribute[])System.Attribute.GetCustomAttributes(cl, typeof(Dapper.TableAttribute));
            if (classCustomAttributes.Length > 0)
            {
                var myAttribute = classCustomAttributes[0];
                return myAttribute.Name;
            }
            else
            {
                return typeof(T).Name;
            }

        }
    }
}
