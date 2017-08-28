using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

[DataObject(true)]
public class DataStore<T> where T : IDataStoreItem, new()
{
    private SqlDB db;

    public DataStore()
    {
        db = new SqlDB(ConfigurationManager.ConnectionStrings["DataStoreConnectionString"].ConnectionString);
    }

    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public T Select()
    {
        return db.GetItem<T>(new T().SelectProcedureName);
    }

    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public IEnumerable<T> SelectAll()
    {
        return db.GetList<T>(new T().SelectAllProcedureName);
    }

    [DataObjectMethod(DataObjectMethodType.Insert, true)]
    public void Insert(T item)
    {
        db.ExecuteSP(item.InsertProcedureName, item.ConvertToDictionary());
    }

    [DataObjectMethod(DataObjectMethodType.Update, true)]
    public void Update(T item)
    {
        db.ExecuteSP(item.UpdateProcedureName, item.ConvertToDictionary());
    }

    [DataObjectMethod(DataObjectMethodType.Delete, true)]
    public void Delete(T item)
    {
        db.ExecuteSP(new T().DeleteProcedureName, new Dictionary<string, object> { { "Id", item.Id} });
    }
}