public class Customer : IDataStoreItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    string IDataStoreItem.SelectProcedureName { get { return "SelectCustomer"; } }
    string IDataStoreItem.SelectAllProcedureName { get { return "SelectAllCustomers"; } }
    string IDataStoreItem.SelectCountProcedureName { get { return "SelectCountCustomers"; } }
    string IDataStoreItem.InsertProcedureName { get { return "InsertCustomer"; } }
    string IDataStoreItem.DeleteProcedureName { get { return "DeleteCustomer"; } }
    string IDataStoreItem.UpdateProcedureName { get { return "UpdateCustomer"; } }
}
