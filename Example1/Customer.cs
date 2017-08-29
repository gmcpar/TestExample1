using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Customer", Schema ="dbo")]
public class Customer : IDataStoreItem
{
    [Key]
    [Column("Id", TypeName ="nvarchar")]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Name", TypeName ="nvarchar")]
    public string Name { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Email", TypeName ="nvarchar")]
    public string Email { get; set; }

    [Required]
    [RegularExpression("", ErrorMessage ="Wrong")]
    [MaxLength(50)]
    [Column("Telephone", TypeName ="nvarchar")]
    public string Telephone { get; set; }

    string IDataStoreItem.SelectProcedureName { get { return "SelectCustomer"; } }
    string IDataStoreItem.SelectAllProcedureName { get { return "SelectAllCustomers"; } }
    string IDataStoreItem.SelectCountProcedureName { get { return "SelectCountCustomers"; } }
    string IDataStoreItem.InsertProcedureName { get { return "InsertCustomer"; } }
    string IDataStoreItem.DeleteProcedureName { get { return "DeleteCustomer"; } }
    string IDataStoreItem.UpdateProcedureName { get { return "UpdateCustomer"; } }
}
