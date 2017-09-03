using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;


namespace DataManager
{
    public class AnnotationValidator : BaseValidator
    {
        public string SourceType { get;  set; }
        public string PropertyName { get; set; }
        protected override bool EvaluateIsValid()
        {
            Type t = Type.GetType(SourceType, true, true);
            PropertyInfo props = t.GetProperty(PropertyName);

            var control = FindControl(ControlToValidate);

            foreach (ValidationAttribute attr in props.GetCustomAttributes())
            {
                if (!attr.IsValid(GetValue(control)))
                {
                    DisplayNameAttribute dna = props.GetCustomAttributes(typeof(DisplayNameAttribute), true).OfType<DisplayNameAttribute>().FirstOrDefault();
                    string err = (dna != null) ? dna.DisplayName : props.Name;
                    ErrorMessage = attr.FormatErrorMessage(err);
                    return false;
                }
            }
            return true;
        }

        private string GetValue(Control c)
        {
            if(c is TextBox) return (c as TextBox).Text;
            if(c is DropDownList) return (c as DropDownList).Text;
            if(c is CheckBox) return (c as CheckBox).Text;
            return string.Empty;
        }
    }
}
