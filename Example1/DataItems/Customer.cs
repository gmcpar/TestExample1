using Example1;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataItem
{
    [Table("Customer", Schema = "dbo")]
    public class Customer : IDataStoreItem
    {
        [Key]
        [Column("Id", TypeName = "int")]
        [Required()]
        [Range(55, int.MaxValue)]
        public int Id { get; set; }

        [Column("Name", TypeName = "nvarchar(50)")]
        [Required()]
        [MaxLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ErrorResources))]
        public string Name { get; set; }


        [Column("Email", TypeName = "nvarchar(50)")]
        [NullableEmailAddress()]
        public string Email { get; set; }

        [Column("Telephone", TypeName = "nvarchar(20)")]
        public string Telephone { get; set; }

        string IDataStoreItem.SelectProcedureName { get { return "SelectCustomer"; } }
        string IDataStoreItem.SelectAllProcedureName { get { return "SelectAllCustomers"; } }
        string IDataStoreItem.SelectCountProcedureName { get { return "SelectCountCustomers"; } }
        string IDataStoreItem.InsertProcedureName { get { return "InsertCustomer"; } }
        string IDataStoreItem.DeleteProcedureName { get { return "DeleteCustomer"; } }
        string IDataStoreItem.UpdateProcedureName { get { return "UpdateCustomer"; } }
    }
}