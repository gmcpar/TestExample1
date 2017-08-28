using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLDB
{  
    [DataObject(true)]
    public class DataManager
    {
        private DB db;

        public DataManager(DB sqldb)
        {
            db = sqldb;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public Object GetAllItems()
        {
            return db.GetList<T>("");
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(T item)
        {

        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public T Update(T item)
        {
            return item;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void Delete(T item)
        {

        }
    }
}
