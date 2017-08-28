public interface IDataStoreItem
{
    int Id { get; set; }
    string SelectProcedureName { get; }
    string SelectAllProcedureName { get; }
    string SelectCountProcedureName { get; }
    string InsertProcedureName { get; }
    string DeleteProcedureName { get; }
    string UpdateProcedureName { get; }
}