using System;
public partial class WebForm1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.DataObjectTypeName = "Customer";
        ObjectDataSource1.TypeName = typeof(DataStore<Customer>).AssemblyQualifiedName;
    }
}