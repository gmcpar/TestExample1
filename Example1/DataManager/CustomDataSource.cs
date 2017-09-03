using System;
using System.Web.UI.WebControls;

namespace DataManager
{
    public class CustomDataSource : ObjectDataSource
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            SetDefaultMethodNames();
            SetCustomTypeName();
            SetObjectParameters();           
        }

        private void SetDefaultMethodNames()
        {
            if (String.IsNullOrEmpty(SelectMethod))
                SelectMethod = "Select";
            DeleteMethod = "Delete";
            UpdateMethod = "Update";
            InsertMethod = "Insert";
        }

        private void SetObjectParameters()
        {
            InsertParameters.Add(new Parameter("item", TypeCode.Object));
            UpdateParameters.Add(new Parameter("item", TypeCode.Object));
            DeleteParameters.Add(new Parameter("item", TypeCode.Object));
        }

        private void SetCustomTypeName()
        {
            Type objecttype = Type.GetType(DataObjectTypeName);
            TypeName = typeof(DataStore<>).MakeGenericType(objecttype).AssemblyQualifiedName;
        }
    }
}