namespace SwissKip.Web.Helpers.ModelBinders
{
    using System;
    using System.Web.Mvc;

    using SwissKip.Web.Models;

    public class BirthdayModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException("bindingContext");

            return new Birthday
                {
                    Year = this.Get(bindingContext, "Year"),
                    Month = this.Get(bindingContext, "Month"),
                    Day = this.Get(bindingContext, "Day")
                };
        }

        private int? Get(ModelBindingContext bindingContext, string key)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + key);
            return (int?)valueResult.ConvertTo(typeof(int?));
        }
    }
}