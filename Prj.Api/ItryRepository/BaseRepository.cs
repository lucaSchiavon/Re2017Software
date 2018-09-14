using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using it.itryframework2.attributes;

namespace ItryRepositories.Repositories.Base
{
   public class BaseRepository<T> :IBaseRepository<T>
        where T : it.itryframework2.interfaces.IGenericObject, new()
    {
        public T[] Get()
        {
            var databaseB2b = new it.itryframework2.data.DBMapper();
            T repo = new T();         
            T[] arrObjects = databaseB2b.select<T>("Select * from " + repo.TableName);
            return arrObjects;
        }
        public T Get(int id)
        {
            var databaseB2b = new it.itryframework2.data.DBMapper();
            T repo = new T();
            T[] arrObjects = databaseB2b.select<T>("Select * from " + repo.TableName + " where " + repo.PrimaryKey + "=" + id);
            return arrObjects[0];
        }

        //restituisce un array di oggetti generici data una query
        public T[] Get(string sql)
        {
            var databaseB2b = new it.itryframework2.data.DBMapper();
            T repo = new T();
            T[] arrObjects = databaseB2b.select<T>(sql);
            return arrObjects;
        }

        public dynamic ExecuteScalar(string sql)
        {
            var databaseB2b = new it.itryframework2.data.DBMapper();

            dynamic Obj = databaseB2b.executeScalar(sql, false);
            return Obj;
        }
        public dynamic ExecuteScalar(string sql,bool GetLastInsertedID)
        {
            var databaseB2b = new it.itryframework2.data.DBMapper();

            dynamic Obj = databaseB2b.executeScalar(sql, false);
            return Obj;
        }
        public System.Data.DataTable GetDatatable(string sql)
        {
            var databaseB2b = new it.itryframework2.data.DBMapper();
          
            System.Data.DataTable dt = databaseB2b.select(sql);
            return dt;
        }

        public T GetLast()
        {
            var databaseB2b = new it.itryframework2.data.DBMapper();
            T repo = new T();
            T[] arrObjects = databaseB2b.select<T>("Select * from " + repo.TableName + " where " + repo.PrimaryKey + "=(select MAX(" + repo.PrimaryKey + ") as valoreMassimo from " + repo.TableName + " as aliasTable)");
            return arrObjects[0];
        }
        public void Delete(int id)
        {
            var databaseB2b = new it.itryframework2.data.DBMapper();
            T repo = new T();
           databaseB2b.executeNonQuery("Delete from " + repo.TableName + " where " + repo.PrimaryKey + "=" + id);
            //return arrObjects[0];
        }
        public void Insert(T obj)
        {
            var databaseB2b = new it.itryframework2.data.DBMapper();
            it.itryframework2.interfaces.IGenericObject GenObj = obj;
            databaseB2b.insert(GenObj);
          
        }
        public void Update(int id,T obj)
        {
            var databaseB2b = new it.itryframework2.data.DBMapper();
           
            //Valorizza la primary key dell'oggetto con reflection
            Type myType = obj.GetType();
            PropertyInfo pinfo = myType.GetProperty(obj.PrimaryKey);
            pinfo.SetValue(obj, id,null);

            it.itryframework2.interfaces.IGenericObject GenObj = obj;
           
            databaseB2b.update(GenObj);

        }
    }
}
