using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace DataManager
{
    [DataObject(true)]
    public class DataStore<T> where T : IDataStoreItem, new()
    {
        private SqlDB db;

        public DataStore()
        {
            if (string.IsNullOrEmpty(Config.ConnectionString.Trim()))
            {
                throw new Exception("A valid connection string must exist in the <connectionStrings> configuration section for the application.");
            }

            db = new SqlDB(Config.ConnectionString);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public T Select()
        {
            return db.GetItem<T>(new T().SelectProcedureName);
        }

        [DataObjectMethod(DataObjectMethodType.Fill, true)]
        public IEnumerable<T> SelectAll()
        {
            return db.GetList<T>(new T().SelectAllProcedureName);
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(T item)
        {
            db.ExecuteSP(item.InsertProcedureName, item);
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(T item)
        {
            db.ExecuteSP(item.UpdateProcedureName, item);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public int SelectCount()
        {
            return db.GetValue<int>(new T().SelectCountProcedureName);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void Delete(T item)
        {
            db.ExecuteSP(new T().DeleteProcedureName, item);
        }
    }
}