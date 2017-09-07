using System.ComponentModel.DataAnnotations;

public class Customer : IDataStoreItem
{
    [Required()]
    [Range(55,int.MaxValue)]
    public int Id { get; set; }

    [Required()]
    [MaxLength(50, ErrorMessageResourceName="StringLength", ErrorMessageResourceType = typeof(Example1.ErrorResources))]
    public string Name { get; set; }

    [NullableEmailAddress()]
    public string Email { get; set; }

    public string Telephone { get; set; }

    /// <summary>
    /// Implementing the IDataStore item interface to provide the stored 
    /// procedure names for the customdatasource
    /// </summary>
    string IDataStoreItem.SelectProcedureName { get { return "SelectCustomer"; } }
    string IDataStoreItem.SelectAllProcedureName { get { return "SelectAllCustomers"; } }
    string IDataStoreItem.SelectCountProcedureName { get { return "SelectCountCustomers"; } }
    string IDataStoreItem.InsertProcedureName { get { return "InsertCustomer"; } }
    string IDataStoreItem.DeleteProcedureName { get { return "DeleteCustomer"; } }
    string IDataStoreItem.UpdateProcedureName { get { return "UpdateCustomer"; } }
}
