using System;
using System.Dynamic;

public partial class WebForm1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.DataObjectTypeName = "Customer";
        ObjectDataSource1.TypeName = typeof(DataStore<Customer>).AssemblyQualifiedName;


        dynamic d = new ExpandoObject();
        d.A = 1;
        d.B = 3;
        d.C = "d";
        tb.Text = d.C;
    }
}