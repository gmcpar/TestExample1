using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;


public class Customer : IDataStoreItem
{
    [Required()]
    [Range(55,int.MaxValue)]
    public int Id { get; set; }

    [Required()]
    [MaxLength(50, ErrorMessageResourceName="StringLength", ErrorMessageResourceType = typeof(Example1.ErrorResources))]
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
